using System;
using System.IO;
using System.Runtime.InteropServices;

namespace mozjpeg.net
{
	public unsafe class StructsJpegLib
	{
		#if __IOS__
		public const string JPEGLIB_PATH = "__Internal";
		#else
		public const string JPEGLIB_PATH = "mozjpeg";
		#endif


		public const int JPEG_LIB_VERSION = 62;
		public const int DCTSIZE = 8;
		public const int DCTSIZE2 = 64;
		public const int NUM_QUANT_TBLS = 4;
		public const int NUM_HUFF_TBLS = 4;
		public const int NUM_ARITH_TBLS = 16;
		public const int MAX_COMPS_IN_SCAN = 4;
		public const int MAX_SAMP_FACTOR = 4;
		public const int C_MAX_BLOCKS_IN_MCU = 10;
		public const int D_MAX_BLOCKS_IN_MCU = 10;

		public enum J_COLOR_SPACE
		{
			JCS_UNKNOWN,
			JCS_GRAYSCALE,
			JCS_RGB,
			JCS_YCbCr,
			JCS_CMYK,
			JCS_YCCK
		};

		public enum J_DCT_METHOD
		{
			JDCT_ISLOW,
			JDCT_IFAST,
			JDCT_FLOAT
		};

		public enum J_DITHER_MODE
		{
			JDITHER_NONE,
			JDITHER_ORDERED,
			JDITHER_FS
		};

		[StructLayout(LayoutKind.Sequential)]
		public struct jpeg_compress_struct
		{
			public IntPtr err;
			[NonSerializedAttribute]
			public void *mem;
			[NonSerializedAttribute]
			public void *progress;
			public IntPtr client_data;
			public int is_decompressor;
			public int global_state;
			[NonSerializedAttribute]
			public IntPtr dest;
			public uint image_width;
			public uint image_height;
			public int input_components;
			public J_COLOR_SPACE in_color_space;
			public double input_gamma;
			public int data_precision;
			public int num_components;
			public J_COLOR_SPACE jpeg_color_space;
			[NonSerializedAttribute]
			public void *comp_info;
			[NonSerializedAttribute]
			public void *quant_tbl_ptrs_0;
			[NonSerializedAttribute]
			public void *quant_tbl_ptrs_1;
			[NonSerializedAttribute]
			public void *quant_tbl_ptrs_2;
			[NonSerializedAttribute]
			public void *quant_tbl_ptrs_3;
			[NonSerializedAttribute]
			public void *dc_huff_tbl_ptrs_0;
			[NonSerializedAttribute]
			public void *dc_huff_tbl_ptrs_1;
			[NonSerializedAttribute]
			public void *dc_huff_tbl_ptrs_2;
			[NonSerializedAttribute]
			public void *dc_huff_tbl_ptrs_3;
			[NonSerializedAttribute]
			public void *ac_huff_tbl_ptrs_0;
			[NonSerializedAttribute]
			public void *ac_huff_tbl_ptrs_1;
			[NonSerializedAttribute]
			public void *ac_huff_tbl_ptrs_2;
			[NonSerializedAttribute]
			public void *ac_huff_tbl_ptrs_3;
			public byte arith_dc_L_0;
			public byte arith_dc_L_1;
			public byte arith_dc_L_2;
			public byte arith_dc_L_3;
			public byte arith_dc_L_4;
			public byte arith_dc_L_5;
			public byte arith_dc_L_6;
			public byte arith_dc_L_7;
			public byte arith_dc_L_8;
			public byte arith_dc_L_9;
			public byte arith_dc_L_10;
			public byte arith_dc_L_11;
			public byte arith_dc_L_12;
			public byte arith_dc_L_13;
			public byte arith_dc_L_14;
			public byte arith_dc_L_15;
			public byte arith_dc_U_0;
			public byte arith_dc_U_1;
			public byte arith_dc_U_2;
			public byte arith_dc_U_3;
			public byte arith_dc_U_4;
			public byte arith_dc_U_5;
			public byte arith_dc_U_6;
			public byte arith_dc_U_7;
			public byte arith_dc_U_8;
			public byte arith_dc_U_9;
			public byte arith_dc_U_10;
			public byte arith_dc_U_11;
			public byte arith_dc_U_12;
			public byte arith_dc_U_13;
			public byte arith_dc_U_14;
			public byte arith_dc_U_15;
			public byte arith_dc_K_0;
			public byte arith_dc_K_1;
			public byte arith_dc_K_2;
			public byte arith_dc_K_3;
			public byte arith_dc_K_4;
			public byte arith_dc_K_5;
			public byte arith_dc_K_6;
			public byte arith_dc_K_7;
			public byte arith_dc_K_8;
			public byte arith_dc_K_9;
			public byte arith_dc_K_10;
			public byte arith_dc_K_11;
			public byte arith_dc_K_12;
			public byte arith_dc_K_13;
			public byte arith_dc_K_14;
			public byte arith_dc_K_15;
			public int num_scans;
			[NonSerializedAttribute]
			public void *scan_info;
			public int raw_data_in;
			public int arith_code;
			public int optimize_coding;
			public int CCIR601_sampling;
			public int smoothing_factor;
			public J_DCT_METHOD dct_method;
			public uint restart_interval;
			public int restart_in_rows;
			public int write_JFIF_header;
			public byte JFIF_major_version;
			public byte JFIF_minor_version;
			public byte density_unit;
			public ushort X_density;
			public ushort Y_density;
			public int write_Adobe_marker;
			public uint next_scanline;
			public int progressive_mode;
			public int max_h_samp_factor;
			public int max_v_samp_factor;
			public uint total_iMCU_rows;
			public int comps_in_scan;
			[NonSerializedAttribute]
			public void *cur_comp_info_0;
			[NonSerializedAttribute]
			public void *cur_comp_info_1;
			[NonSerializedAttribute]
			public void *cur_comp_info_2;
			[NonSerializedAttribute]
			public void *cur_comp_info_3;
			public uint MCUs_per_row;
			public uint MCU_rows_in_scan;
			public int blocks_in_MCU;
			public int MCU_membership_0;
			public int MCU_membership_1;
			public int MCU_membership_2;
			public int MCU_membership_3;
			public int MCU_membership_4;
			public int MCU_membership_5;
			public int MCU_membership_6;
			public int MCU_membership_7;
			public int MCU_membership_8;
			public int MCU_membership_9;
			public int Ss, Se, Ah, Al;
			[NonSerializedAttribute]
			public void *master;
			[NonSerializedAttribute]
			public void *main;
			[NonSerializedAttribute]
			public void *prep;
			[NonSerializedAttribute]
			public void *coef;
			[NonSerializedAttribute]
			public void *marker;
			[NonSerializedAttribute]
			public void *cconvert;
			[NonSerializedAttribute]
			public void *downsample;
			[NonSerializedAttribute]
			public void *fdct;
			[NonSerializedAttribute]
			public void *entropy;
			[NonSerializedAttribute]
			public void *script_space;
			public int script_space_size;

		};

		[StructLayout(LayoutKind.Sequential)]
		public struct jpeg_decompress_struct
		{
			public IntPtr err;
			[NonSerializedAttribute]
			public void *mem;
			[NonSerializedAttribute]
			public void *progress;
			public IntPtr client_data;
			public int is_decompressor;
			public int global_state;
			[NonSerializedAttribute]
			public IntPtr src;
			public uint image_width;
			public uint image_height;
			public int num_components;
			public J_COLOR_SPACE jpeg_color_space;
			public J_COLOR_SPACE out_color_space;
			public uint scale_num, scale_denom;
			public double output_gamma;
			public int buffered_image;
			public int raw_data_out;
			public J_DCT_METHOD dct_method;
			public int do_fancy_upsampling;
			public int do_block_smoothing;
			public int quantize_colors;
			public J_DITHER_MODE dither_mode;
			public int two_pass_quantize;
			public int desired_number_of_colors;
			public int enable_1pass_quant;
			public int enable_external_quant;
			public int enable_2pass_quant;
			public uint output_width;
			public uint output_height;
			public int out_color_components;
			public int output_components;
			public int rec_outbuf_height;
			public int actual_number_of_colors;
			[NonSerializedAttribute]
			public void *colormap;
			public uint output_scanline;
			public int input_scan_number;
			public uint input_iMCU_row;
			public int output_scan_number;
			public uint output_iMCU_row;
			[NonSerializedAttribute]
			public void *coef_bits;
			[NonSerializedAttribute]
			public void *quant_tbl_ptrs_0;
			[NonSerializedAttribute]
			public void *quant_tbl_ptrs_1;
			[NonSerializedAttribute]
			public void *quant_tbl_ptrs_2;
			[NonSerializedAttribute]
			public void *quant_tbl_ptrs_3;
			[NonSerializedAttribute]
			public void *dc_huff_tbl_ptrs_0;
			[NonSerializedAttribute]
			public void *dc_huff_tbl_ptrs_1;
			[NonSerializedAttribute]
			public void *dc_huff_tbl_ptrs_2;
			[NonSerializedAttribute]
			public void *dc_huff_tbl_ptrs_3;
			[NonSerializedAttribute]
			public void *ac_huff_tbl_ptrs_0;
			[NonSerializedAttribute]
			public void *ac_huff_tbl_ptrs_1;
			[NonSerializedAttribute]
			public void *ac_huff_tbl_ptrs_2;
			[NonSerializedAttribute]
			public void *ac_huff_tbl_ptrs_3;
			public int data_precision;
			[NonSerializedAttribute]
			public void *comp_info;
			public int progressive_mode;
			public int arith_code;
			public byte arith_dc_L_0;
			public byte arith_dc_L_1;
			public byte arith_dc_L_2;
			public byte arith_dc_L_3;
			public byte arith_dc_L_4;
			public byte arith_dc_L_5;
			public byte arith_dc_L_6;
			public byte arith_dc_L_7;
			public byte arith_dc_L_8;
			public byte arith_dc_L_9;
			public byte arith_dc_L_10;
			public byte arith_dc_L_11;
			public byte arith_dc_L_12;
			public byte arith_dc_L_13;
			public byte arith_dc_L_14;
			public byte arith_dc_L_15;
			public byte arith_dc_U_0;
			public byte arith_dc_U_1;
			public byte arith_dc_U_2;
			public byte arith_dc_U_3;
			public byte arith_dc_U_4;
			public byte arith_dc_U_5;
			public byte arith_dc_U_6;
			public byte arith_dc_U_7;
			public byte arith_dc_U_8;
			public byte arith_dc_U_9;
			public byte arith_dc_U_10;
			public byte arith_dc_U_11;
			public byte arith_dc_U_12;
			public byte arith_dc_U_13;
			public byte arith_dc_U_14;
			public byte arith_dc_U_15;
			public byte arith_dc_K_0;
			public byte arith_dc_K_1;
			public byte arith_dc_K_2;
			public byte arith_dc_K_3;
			public byte arith_dc_K_4;
			public byte arith_dc_K_5;
			public byte arith_dc_K_6;
			public byte arith_dc_K_7;
			public byte arith_dc_K_8;
			public byte arith_dc_K_9;
			public byte arith_dc_K_10;
			public byte arith_dc_K_11;
			public byte arith_dc_K_12;
			public byte arith_dc_K_13;
			public byte arith_dc_K_14;
			public byte arith_dc_K_15;
			public uint restart_interval;
			public int saw_JFIF_marker;
			public byte JFIF_major_version;
			public byte JFIF_minor_version;
			public byte density_unit;
			public ushort X_density;
			public ushort Y_density;
			public int saw_Adobe_marker;
			public byte Adobe_transform;
			public int CCIR601_sampling;
			[NonSerializedAttribute]
			public void *marker_list;
			public int max_h_samp_factor;
			public int max_v_samp_factor;
			public int min_DCT_scaled_size;
			public uint total_iMCU_rows;
			[NonSerializedAttribute]
			public void *sample_range_limit;
			public int comps_in_scan;
			[NonSerializedAttribute]
			public void *cur_comp_info_0;
			[NonSerializedAttribute]
			public void *cur_comp_info_1;
			[NonSerializedAttribute]
			public void *cur_comp_info_2;
			[NonSerializedAttribute]
			public void *cur_comp_info_3;
			public uint MCUs_per_row;
			public uint MCU_rows_in_scan;
			public int blocks_in_MCU;
			public int MCU_membership_0;
			public int MCU_membership_1;
			public int MCU_membership_2;
			public int MCU_membership_3;
			public int MCU_membership_4;
			public int MCU_membership_5;
			public int MCU_membership_6;
			public int MCU_membership_7;
			public int MCU_membership_8;
			public int MCU_membership_9;
			public int Ss, Se, Ah, Al;
			public int unread_marker;
			[NonSerializedAttribute]
			public void *master;
			[NonSerializedAttribute]
			public void *main;
			[NonSerializedAttribute]
			public void *coef;
			[NonSerializedAttribute]
			public void *post;
			[NonSerializedAttribute]
			public void *inputctl;
			[NonSerializedAttribute]
			public void *marker;
			[NonSerializedAttribute]
			public void *entropy;
			[NonSerializedAttribute]
			public void *idct;
			[NonSerializedAttribute]
			public void *upsample;
			[NonSerializedAttribute]
			public void *cconvert;
			[NonSerializedAttribute]
			public void *cquantize;

		}; 

		[DllImport(StructsJpegLib.JPEGLIB_PATH)]
		extern public static void jpeg_CreateCompress
			(ref jpeg_compress_struct cinfo, int version, IntPtr structsize);

		public static void jpeg_create_compress(ref jpeg_compress_struct cinfo)
		{
			jpeg_CreateCompress(ref cinfo, JPEG_LIB_VERSION, (IntPtr)Marshal.SizeOf<jpeg_compress_struct>());
		}

		[DllImport(StructsJpegLib.JPEGLIB_PATH)]
		extern public static void jpeg_CreateDecompress
		(ref jpeg_decompress_struct cinfo, int version, IntPtr structsize);

		public static void jpeg_create_decompress(ref jpeg_decompress_struct cinfo)
		{
			jpeg_CreateDecompress (ref cinfo, JPEG_LIB_VERSION, (IntPtr)Marshal.SizeOf<jpeg_decompress_struct> ());
		}

		[DllImport(StructsJpegLib.JPEGLIB_PATH)]
		extern public static void jpeg_destroy_compress
			(ref jpeg_compress_struct cinfo);

		[DllImport(StructsJpegLib.JPEGLIB_PATH)]
		extern public static void jpeg_destroy_decompress
			(ref jpeg_decompress_struct cinfo);

		[DllImport(StructsJpegLib.JPEGLIB_PATH)]
		extern public static void jpeg_set_defaults
			(ref jpeg_compress_struct cinfo);

		[DllImport(StructsJpegLib.JPEGLIB_PATH)]
		extern public static void jpeg_set_colorspace
			(ref jpeg_compress_struct cinfo, J_COLOR_SPACE colorspace);

		[DllImport(StructsJpegLib.JPEGLIB_PATH)]
		extern public static void jpeg_default_colorspace
			(ref jpeg_compress_struct cinfo);

		[DllImport(StructsJpegLib.JPEGLIB_PATH)]
		extern public static void jpeg_set_quality
			(ref jpeg_compress_struct cinfo, int quality, int force_baseline);

		[DllImport(StructsJpegLib.JPEGLIB_PATH)]
		extern public static void jpeg_set_linear_quality
			(ref jpeg_compress_struct cinfo, int scale_factor, int force_baseline);

		[DllImport(StructsJpegLib.JPEGLIB_PATH)]
		extern public static void jpeg_add_quant_table
			(ref jpeg_compress_struct cinfo, int which_tbl,	void *basic_table, int scale_factor, int force_baseline);

		[DllImport(StructsJpegLib.JPEGLIB_PATH)]
		extern public static int jpeg_quality_scaling(int quality);

		[DllImport(StructsJpegLib.JPEGLIB_PATH)]
		extern public static void jpeg_simple_progression
			(ref jpeg_compress_struct cinfo);

		[DllImport(StructsJpegLib.JPEGLIB_PATH)]
		extern public static void jpeg_suppress_tables
			(ref jpeg_compress_struct cinfo, int suppress);

		[DllImport(StructsJpegLib.JPEGLIB_PATH)]
		extern public static void *jpeg_alloc_quant_table
			(ref jpeg_compress_struct cinfo);

		[DllImport(StructsJpegLib.JPEGLIB_PATH)]
		extern public static void *jpeg_alloc_quant_table
			(ref jpeg_decompress_struct cinfo);

		[DllImport(StructsJpegLib.JPEGLIB_PATH)]
		extern public static void *jpeg_alloc_huff_table
			(ref jpeg_compress_struct cinfo);

		[DllImport(StructsJpegLib.JPEGLIB_PATH)]
		extern public static void *jpeg_alloc_huff_table
			(ref jpeg_decompress_struct cinfo);

		[DllImport(StructsJpegLib.JPEGLIB_PATH)]
		extern public static void jpeg_start_compress
			(ref jpeg_compress_struct cinfo, int write_all_tables);

		[DllImport(StructsJpegLib.JPEGLIB_PATH)]
		extern public static uint jpeg_write_scanlines
			(ref jpeg_compress_struct cinfo,ref IntPtr scanline, uint num_lines);

		[DllImport(StructsJpegLib.JPEGLIB_PATH)]
		extern public static void jpeg_finish_compress
			(ref jpeg_compress_struct cinfo);

		[DllImport(StructsJpegLib.JPEGLIB_PATH)]
		extern public static void jpeg_write_tables
			(ref jpeg_compress_struct cinfo);

		[DllImport(StructsJpegLib.JPEGLIB_PATH)]
		extern public static void jpeg_calc_output_dimensions
			(ref jpeg_decompress_struct cinfo);

		[DllImport(StructsJpegLib.JPEGLIB_PATH)]
		extern public static int jpeg_read_header
			(ref jpeg_decompress_struct cinfo, int require_image);

		public const int JPEG_SUSPENDED = (int)0;
		public const int JPEG_HEADER_OK = (int)1;
		public const int JPEG_HEADER_TABLES_ONLY = (int)2;

		[DllImport(StructsJpegLib.JPEGLIB_PATH)]
		extern public static int jpeg_start_decompress
			(ref jpeg_decompress_struct cinfo);

		[DllImport(StructsJpegLib.JPEGLIB_PATH)]
		extern public static uint jpeg_read_scanlines
			(ref jpeg_decompress_struct cinfo, ref IntPtr scanline, uint max_lines);

		[DllImport(StructsJpegLib.JPEGLIB_PATH)]
		extern public static int jpeg_finish_decompress
			(ref jpeg_decompress_struct cinfo);

		[DllImport(StructsJpegLib.JPEGLIB_PATH)]
		extern public static void jpeg_abort_compress
			(ref jpeg_compress_struct cinfo);

		[DllImport(StructsJpegLib.JPEGLIB_PATH)]
		extern public static void jpeg_abort_decompress
			(ref jpeg_decompress_struct cinfo);

		[DllImport(StructsJpegLib.JPEGLIB_PATH)]
		extern public static int jpeg_resync_to_restart
			(ref jpeg_decompress_struct cinfo, int desired);

		[DllImport(StructsJpegLib.JPEGLIB_PATH)]
		extern public static IntPtr jpeg_std_error(IntPtr err);

		[DllImport(StructsJpegLib.JPEGLIB_PATH)]
		extern public static void jpeg_mem_dest
			(ref jpeg_compress_struct cinfo, ref IntPtr outbuffer, ref ulong outsize);

		[DllImport(StructsJpegLib.JPEGLIB_PATH)]
		extern public static void jpeg_mem_dest_free
			(ref jpeg_compress_struct cinfo);

		[DllImport(StructsJpegLib.JPEGLIB_PATH)]
		extern public static void jpeg_mem_src
			(ref jpeg_decompress_struct cinfo, IntPtr inbuffer, ulong insize);
	};


	public class MemoryManager 
	{
		public static IntPtr CreateErrorHandler()
		{
			IntPtr err = Marshal.AllocHGlobal(512);
			return StructsJpegLib.jpeg_std_error(err);
		}

		public static void FreeErrorHandler(IntPtr err)
		{
			Marshal.FreeHGlobal(err);
		}
	}
};