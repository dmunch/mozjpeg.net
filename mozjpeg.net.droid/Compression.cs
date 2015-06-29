using System;
using System.Runtime.InteropServices;
using Android.Graphics;

namespace mozjpeg.net
{
    public partial class Compression
    {
        public unsafe static byte[] EncodeFromArgb8888(Bitmap bitmap, int quality = 100, bool useMozjpeg = false)
        {
            if (bitmap.GetConfig() != Bitmap.Config.Argb8888)
            {
                throw new NotSupportedException("Bitmap configuration must be Argb8888");
            }

            #if DEBUG
            System.Diagnostics.Debug.WriteLine("Picture size before {0} kb", bitmap.ByteCount / (float)1024);
            var sw = new System.Diagnostics.Stopwatch(); sw.Start();
            #endif

            var intPtr = bitmap.LockPixels();
            var data = EncodeFromArgb8888(intPtr, (uint)bitmap.Width, (uint)bitmap.Height, quality, useMozjpeg);
            bitmap.UnlockPixels();

            #if DEBUG
            sw.Stop(); 
            System.Diagnostics.Debug.WriteLine("Encoding took {0}", sw.Elapsed);
            System.Diagnostics.Debug.WriteLine("Picture size mozjpeg {0} kb", data.Length / (float)1024);
            #endif

            return data;
        }

        public unsafe static byte[] EncodeFromArgb8888(IntPtr data, uint width, uint height, int quality = 100, bool useMozjpeg = false)
        {
            StructsJpegLib.jpeg_compress_struct cinfo;
            cinfo = new StructsJpegLib.jpeg_compress_struct();
            cinfo.err = MemoryManager.CreateErrorHandler();
            StructsJpegLib.jpeg_create_compress(ref cinfo);

            IntPtr outbuffer = IntPtr.Zero;
            ulong outsize = 0;
            StructsJpegLib.jpeg_mem_dest(ref cinfo, ref outbuffer, ref outsize);

            // Set the JPEG compression parameters.
            cinfo.image_width = width;
            cinfo.image_height = height;

            cinfo.input_components = (int)4;
            cinfo.in_color_space = StructsJpegLib.J_COLOR_SPACE.JCS_EXT_RGBA;

            if (StructsExtensions.jpeg_c_int_param_supported (ref cinfo, StructsExtensions.J_INT_PARAM.JINT_COMPRESS_PROFILE) == 1) {
                var profileValue = useMozjpeg 
                    ? StructsExtensions.JINT_COMPRESS_PROFILE_VALUES.JCP_MAX_COMPRESSION
                    : StructsExtensions.JINT_COMPRESS_PROFILE_VALUES.JCP_FASTEST;

                StructsExtensions.jpeg_c_set_int_param (ref cinfo, 
                    StructsExtensions.J_INT_PARAM.JINT_COMPRESS_PROFILE, 
                    (int)profileValue);
            }

            StructsJpegLib.jpeg_set_defaults(ref cinfo);
            StructsJpegLib.jpeg_set_quality (ref cinfo, quality, 1);

            StructsJpegLib.jpeg_start_compress(ref cinfo, (int)1);

            uint offset = 0;
            uint stride = width * (uint)cinfo.input_components;
            byte* ptr = (byte*)data;
            while(cinfo.next_scanline < cinfo.image_height)
            {
                offset = cinfo.next_scanline * stride;

                IntPtr buf = (IntPtr)(ptr + offset);
                StructsJpegLib.jpeg_write_scanlines(ref cinfo, ref buf, (uint)1);
            }

            // Finish the compression process.
            StructsJpegLib.jpeg_finish_compress(ref cinfo);

            // Clean everything up.
            //JpegLib.FreeDestinationManager(ref cinfo);
            byte[] compressedData = new byte[outsize];
            Marshal.Copy (outbuffer, compressedData, 0, (int)outsize);

            StructsJpegLib.jpeg_destroy_compress(ref cinfo);
            MemoryManager.FreeErrorHandler(cinfo.err);

            return compressedData;
        }
    }
}

