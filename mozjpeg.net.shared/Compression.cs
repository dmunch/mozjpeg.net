using System;
using System.IO;
using System.Runtime.InteropServices;

namespace mozjpeg.net
{
	public partial class Compression
	{
		public static byte[] DecodeToRGB(byte[] bytes, out uint width, out uint height)
		{
			StructsJpegLib.jpeg_decompress_struct cinfo;
			cinfo = new StructsJpegLib.jpeg_decompress_struct ();
			cinfo.err = MemoryManager.CreateErrorHandler ();

			StructsJpegLib.jpeg_create_decompress (ref cinfo);
			cinfo.do_fancy_upsampling = 0;
			cinfo.dct_method = StructsJpegLib.J_DCT_METHOD.JDCT_IFAST;

			//put these to scale on decompression
			//cinfo.scale_num = 1;
			//cinfo.scale_denom = 8;

			GCHandle bytesPinned = GCHandle.Alloc (bytes, GCHandleType.Pinned);
			StructsJpegLib.jpeg_mem_src (ref cinfo, bytesPinned.AddrOfPinnedObject (), (ulong)bytes.Length);
			StructsJpegLib.jpeg_read_header (ref cinfo, (int)1);

			cinfo.out_color_space = StructsJpegLib.J_COLOR_SPACE.JCS_RGB;

			StructsJpegLib.jpeg_start_decompress (ref cinfo);

			width = cinfo.output_width;
			height = cinfo.output_height;

			uint offset = 0;
			uint stride = width * 3;
			byte[] data = new byte[width * height * 3];

			IntPtr buf = Marshal.AllocHGlobal ((int)stride);
			while (cinfo.output_scanline < cinfo.output_height) {
				uint read_scanlines = StructsJpegLib.jpeg_read_scanlines (ref cinfo, ref buf, (uint)1);

				Marshal.Copy (buf, data, (int)offset, (int)(stride * read_scanlines));
				offset += stride * read_scanlines;
			}

			StructsJpegLib.jpeg_finish_decompress (ref cinfo);
			bytesPinned.Free ();
			StructsJpegLib.jpeg_destroy_decompress (ref cinfo);
			MemoryManager.FreeErrorHandler (cinfo.err);

			return data;
		}

		public static byte[] EncodeFromRGB(byte[] data, uint width, uint height, int quality = 100, bool useMozjpeg = false)
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
			cinfo.input_components = (int)3;
			cinfo.in_color_space = StructsJpegLib.J_COLOR_SPACE.JCS_RGB;

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
			//cinfo.optimize_coding = 0;

			StructsJpegLib.jpeg_start_compress(ref cinfo, (int)1);

			uint offset = 0;
			uint stride = width * 3;
			IntPtr buf = Marshal.AllocHGlobal((int)stride);
			while(cinfo.next_scanline <	cinfo.image_height)
			{
				offset = cinfo.next_scanline * stride;
				Marshal.Copy(data, (int)offset, buf, (int)stride);

				StructsJpegLib.jpeg_write_scanlines(ref cinfo, ref buf, (uint)1);
			}
			Marshal.FreeHGlobal(buf);

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
	};
};
