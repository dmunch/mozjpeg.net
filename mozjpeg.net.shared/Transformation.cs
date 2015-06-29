using System;
using System.IO;
using System.Runtime.InteropServices;

namespace mozjpeg.net
{
	public class Transformation
	{
        public static byte[] Rotate (byte[] bytes)
        {
            return Transform(bytes, StructsTransformations.JXFORM_CODE.JXFORM_ROT_90);
        }

		public static byte[] Transform (byte[] bytes, StructsTransformations.JXFORM_CODE transformationCode)
		{
			StructsTransformations.jpeg_transform_info transformoption;
			transformoption = new StructsTransformations.jpeg_transform_info ();

            transformoption.transform = transformationCode;
			transformoption.perfect = 0;
			transformoption.trim = 0;
			transformoption.force_grayscale = 0;
			transformoption.crop = 0;
			transformoption.slow_hflip = 0;

			StructsJpegLib.jpeg_decompress_struct cinfo;
			cinfo = new StructsJpegLib.jpeg_decompress_struct();
			cinfo.err = MemoryManager.CreateErrorHandler();

			StructsJpegLib.jpeg_create_decompress(ref cinfo);

			StructsJpegLib.jpeg_compress_struct dstInfo;
			dstInfo = new StructsJpegLib.jpeg_compress_struct ();
			dstInfo.err = MemoryManager.CreateErrorHandler ();

			StructsJpegLib.jpeg_create_compress (ref dstInfo);

			GCHandle bytesPinned = GCHandle.Alloc (bytes, GCHandleType.Pinned);
			StructsJpegLib.jpeg_mem_src(ref cinfo, bytesPinned.AddrOfPinnedObject(), (ulong)bytes.Length);
			StructsTransformations.jcopy_markers_setup (ref cinfo, StructsTransformations.JCOPY_OPTION.JCOPYOPT_ALL);


			StructsJpegLib.jpeg_read_header(ref cinfo, (int)1);

			if (StructsTransformations.jtransform_request_workspace (ref cinfo, ref transformoption) == 0) {
				throw new Exception ("Transformation is not perfect");
			}

			IntPtr src_coef_arrays = StructsTransformations.jpeg_read_coefficients (ref cinfo);

			StructsTransformations.jpeg_copy_critical_parameters(ref cinfo, ref dstInfo);
			IntPtr dst_coef_arrays = StructsTransformations.jtransform_adjust_parameters(ref cinfo, ref dstInfo,
				src_coef_arrays,
				ref transformoption);

			IntPtr outbuffer = IntPtr.Zero;
			ulong outsize = 0;
			StructsJpegLib.jpeg_mem_dest(ref dstInfo, ref outbuffer, ref outsize);
			/* Start compressor (note no image data is actually written here) */
			StructsTransformations.jpeg_write_coefficients(ref dstInfo, dst_coef_arrays);

			/* Copy to the output file any extra markers that we want to preserve */
			StructsTransformations.jcopy_markers_execute(ref cinfo, ref dstInfo, StructsTransformations.JCOPY_OPTION.JCOPYOPT_ALL);

			/* Execute image transformation, if any */
			StructsTransformations.jtransform_execute_transform(ref cinfo, ref dstInfo,
			src_coef_arrays, ref transformoption);
			
			/* Finish compression and release memory */
			StructsJpegLib.jpeg_finish_compress(ref dstInfo);

			byte[] data = new byte[outsize];
			Marshal.Copy (outbuffer, data, 0, (int)outsize);
			//TODO check if we leak memory i.e. if outbuffer really is freed
			//JpegLib.jpeg_mem_dest_free(ref dstInfo);
			//Marshal.FreeHGlobal (outbuffer);
			StructsJpegLib.jpeg_destroy_compress(ref dstInfo);

			StructsJpegLib.jpeg_finish_decompress(ref cinfo);
			StructsJpegLib.jpeg_destroy_decompress(ref cinfo);

			return data;
		}
	};
};
