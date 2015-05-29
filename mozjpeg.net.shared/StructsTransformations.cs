using System;
using System.Runtime.InteropServices;

namespace mozjpeg.net
{
	internal unsafe sealed partial class StructsTransformations
	{
		/*
		 * Codes for supported types of image transformations.
		 */

		public enum JXFORM_CODE {
			JXFORM_NONE,            /* no transformation */
			JXFORM_FLIP_H,          /* horizontal flip */
			JXFORM_FLIP_V,          /* vertical flip */
			JXFORM_TRANSPOSE,       /* transpose across UL-to-LR axis */
			JXFORM_TRANSVERSE,      /* transpose across UR-to-LL axis */
			JXFORM_ROT_90,          /* 90-degree clockwise rotation */
			JXFORM_ROT_180,         /* 180-degree rotation */
			JXFORM_ROT_270          /* 270-degree clockwise (or 90 ccw) */
		};

		public enum JCROP_CODE {
			JCROP_UNSET,
			JCROP_POS, 
			JCROP_NEG,
			JCROP_FORCE 
		};

		[StructLayout(LayoutKind.Sequential)]
		public struct jpeg_transform_info{
			/* Options: set by caller */

			public JXFORM_CODE transform;    	/* image transform operator */
			public int perfect;              	/* if TRUE, fail if partial MCUs are requested */
			public int trim;                 	/* if TRUE, trim partial MCUs as needed */
			public int force_grayscale;      	/* if TRUE, convert color image to grayscale */
			public int crop;                 	/* if TRUE, crop source image */
			public int slow_hflip; 			 	/* For best performance, the JXFORM_FLIP_H transform
							                      normally modifies the source coefficients in place.
							                      Setting this to TRUE will instead use a slower,
							                      double-buffered algorithm, which leaves the source
							                      coefficients in tact (necessary if other transformed
							                      images must be generated from the same set of
							                      coefficients. */

			/* Crop parameters: application need not set these unless crop is TRUE.
			*  These can be filled in by jtransform_parse_crop_spec(). */

			public uint crop_width;       		/* Width of selected region */
			public JCROP_CODE crop_width_set;   /* (forced disables adjustment) */
			public uint crop_height;      		/* Height of selected region */
			public JCROP_CODE crop_height_set;  /* (forced disables adjustment) */
			public uint crop_xoffset;     		/* X offset of selected region */
			public JCROP_CODE crop_xoffset_set; /* (negative measures from right edge) */
			public uint crop_yoffset;     		/* Y offset of selected region */
			public JCROP_CODE crop_yoffset_set; /* (negative measures from bottom edge) */

			/* Internal workspace: caller should not touch these */
			public int num_components;    		/* # of components in workspace */
			IntPtr workspace_coef_arrays; 		/* workspace for transformations */
			public uint output_width;     		/* cropped destination dimensions */
			public uint output_height;
			public uint x_crop_offset;    		/* destination crop offsets measured in iMCUs */
			public uint y_crop_offset;
			public int iMCU_sample_width; 		/* destination iMCU size */
			public int iMCU_sample_height;
		}

		[DllImport(StructsJpegLib.JPEGLIB_PATH)]
		extern public static IntPtr jpeg_read_coefficients
			(ref StructsJpegLib.jpeg_decompress_struct cinfo);

		[DllImport(StructsJpegLib.JPEGLIB_PATH)]
		extern public static void jpeg_write_coefficients 
			(ref StructsJpegLib.jpeg_compress_struct destInfo, IntPtr coef_arrays);

		[DllImport(StructsJpegLib.JPEGLIB_PATH)]
		extern public static void jpeg_copy_critical_parameters 
			(ref StructsJpegLib.jpeg_decompress_struct srcinfo,	ref StructsJpegLib.jpeg_compress_struct dstinfo);
		
		[DllImport(StructsJpegLib.JPEGLIB_PATH)]
		/* Parse a crop specification (written in X11 geometry style) */
		extern public static int jtransform_parse_crop_spec
			(ref jpeg_transform_info info, byte *spec);

		/* Request any required workspace */
		[DllImport(StructsJpegLib.JPEGLIB_PATH)]
		extern public static int jtransform_request_workspace
			(ref StructsJpegLib.jpeg_decompress_struct srcinfo, ref jpeg_transform_info info);


		/* Adjust output image parameters */
		[DllImport(StructsJpegLib.JPEGLIB_PATH)]
		extern public static IntPtr jtransform_adjust_parameters
			(ref StructsJpegLib.jpeg_decompress_struct srcinfo, ref StructsJpegLib.jpeg_compress_struct dstinfo,
				IntPtr src_coef_arrays, ref jpeg_transform_info info);
		
		[DllImport(StructsJpegLib.JPEGLIB_PATH)]
		/* Execute the actual transformation, if any */
		extern public static void jtransform_execute_transform
			(ref StructsJpegLib.jpeg_decompress_struct srcinfo, ref StructsJpegLib.jpeg_compress_struct dstinfo,
				IntPtr src_coef_arrays, ref jpeg_transform_info info);

		/* Determine whether lossless transformation is perfectly
	 	 * possible for a specified image and transformation. */
		[DllImport(StructsJpegLib.JPEGLIB_PATH)]
		extern public static int jtransform_perfect_transform
			(uint image_width, uint image_height, int MCU_width, int MCU_height, JXFORM_CODE transform);


		/*
		 * Support for copying optional markers from source to destination file.
 		 */

		public enum JCOPY_OPTION {
			JCOPYOPT_NONE,          /* copy no optional markers */
			JCOPYOPT_COMMENTS,      /* copy only comment (COM) markers */
			JCOPYOPT_ALL            /* copy all optional markers */
		}

		//#define JCOPYOPT_DEFAULT  JCOPYOPT_COMMENTS     /* recommended default */

		/* Setup decompression object to save desired markers in memory */
		[DllImport(StructsJpegLib.JPEGLIB_PATH)]
		extern public static void jcopy_markers_setup
			(ref StructsJpegLib.jpeg_decompress_struct srcinfo, JCOPY_OPTION option);

		/* Copy markers saved in the given source object to the destination object */
		[DllImport(StructsJpegLib.JPEGLIB_PATH)]
		extern public static void jcopy_markers_execute
			(ref StructsJpegLib.jpeg_decompress_struct srcinfo, ref StructsJpegLib.jpeg_compress_struct dstinfo, JCOPY_OPTION option);
	}
}

