using System;
using System.Runtime.InteropServices;

namespace mozjpeg.net
{
	public static class StructsExtensions
	{
		public enum J_BOOLEAN_PARAM : uint {
			JBOOLEAN_OPTIMIZE_SCANS = 0x680C061E, /* TRUE=optimize progressive coding scans */
			JBOOLEAN_TRELLIS_QUANT = 0xC5122033, /* TRUE=use trellis quantization */
			JBOOLEAN_TRELLIS_QUANT_DC = 0x339D4C0C, /* TRUE=use trellis quant for DC coefficient */
			JBOOLEAN_TRELLIS_EOB_OPT = 0xD7F73780, /* TRUE=optimize for sequences of EOB */
			JBOOLEAN_USE_LAMBDA_WEIGHT_TBL = 0x339DB65F, /* TRUE=use lambda weighting table */
			JBOOLEAN_USE_SCANS_IN_TRELLIS = 0xFD841435, /* TRUE=use scans in trellis optimization */
			JBOOLEAN_TRELLIS_Q_OPT = 0xE12AE269, /* TRUE=optimize quant table in trellis loop */
			JBOOLEAN_OVERSHOOT_DERINGING = 0x3F4BBBF9 /* TRUE=preprocess input to reduce ringing of edges on white background */
		};

		public enum J_FLOAT_PARAM : uint {
			JFLOAT_LAMBDA_LOG_SCALE1 = 0x5B61A599,
			JFLOAT_LAMBDA_LOG_SCALE2 = 0xB9BBAE03,
			JFLOAT_TRELLIS_DELTA_DC_WEIGHT = 0x13775453
		};

		public enum J_INT_PARAM : uint {
			JINT_COMPRESS_PROFILE = 0xE9918625, /* compression profile */
			JINT_TRELLIS_FREQ_SPLIT = 0x6FAFF127, /* splitting point for frequency in trellis quantization */
			JINT_TRELLIS_NUM_LOOPS = 0xB63EBF39, /* number of trellis loops */
			JINT_BASE_QUANT_TBL_IDX = 0x44492AB1, /* base quantization table index */
			JINT_DC_SCAN_OPT_MODE = 0x0BE7AD3C /* DC scan optimization mode */
		};


		/* Values for the JINT_COMPRESS_PROFILE parameter (32-bit GUIDs) */
		public enum JINT_COMPRESS_PROFILE_VALUES : Int32 {
			JCP_MAX_COMPRESSION = 0x5D083AAD, /* best compression ratio (progressive, all mozjpeg extensions) */
			JCP_FASTEST = 0x2AEA5CB4 /* libjpeg[-turbo] defaults (baseline, no mozjpeg extensions) */
		};

		[DllImport(StructsJpegLib.JPEGLIB_PATH)]
		extern public static int jpeg_c_bool_param_supported
			(ref StructsJpegLib.jpeg_compress_struct cinfo, J_BOOLEAN_PARAM param);

		[DllImport(StructsJpegLib.JPEGLIB_PATH)]
		extern public static void jpeg_c_set_bool_param
			(ref StructsJpegLib.jpeg_compress_struct cinfo, J_BOOLEAN_PARAM param, int value);

		[DllImport(StructsJpegLib.JPEGLIB_PATH)]
		extern public static int jpeg_c_get_bool_param
			(ref StructsJpegLib.jpeg_compress_struct cinfo, J_BOOLEAN_PARAM param);

		[DllImport(StructsJpegLib.JPEGLIB_PATH)]
		extern public static int jpeg_c_float_param_supported 
			(ref StructsJpegLib.jpeg_compress_struct cinfo, J_FLOAT_PARAM param);

		[DllImport(StructsJpegLib.JPEGLIB_PATH)]
		extern public static void jpeg_c_set_float_param
			(ref StructsJpegLib.jpeg_compress_struct cinfo, J_FLOAT_PARAM param, float value);

		[DllImport(StructsJpegLib.JPEGLIB_PATH)]
		extern public static float jpeg_c_get_float_param
			(ref StructsJpegLib.jpeg_compress_struct cinfo, J_FLOAT_PARAM param);

		[DllImport(StructsJpegLib.JPEGLIB_PATH)]
		extern public static int jpeg_c_int_param_supported 
			(ref StructsJpegLib.jpeg_compress_struct cinfo, J_INT_PARAM param);

		[DllImport(StructsJpegLib.JPEGLIB_PATH)]
		extern public static void jpeg_c_set_int_param 
			(ref StructsJpegLib.jpeg_compress_struct cinfo, J_INT_PARAM param, int value);

		[DllImport(StructsJpegLib.JPEGLIB_PATH)]
		extern public static int jpeg_c_get_int_param 
			(ref StructsJpegLib.jpeg_compress_struct cinfo, J_INT_PARAM param);
	}
}

