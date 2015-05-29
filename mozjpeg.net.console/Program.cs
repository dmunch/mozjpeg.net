using System;
using System.IO;

namespace mozjpeg.net.console
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var bytes = System.IO.File.ReadAllBytes ("./image.jpg");

			var sw = new System.Diagnostics.Stopwatch ();

			sw.Start ();
			var bytesAfter = mozjpeg.net.Transformation.Rotate (bytes);
			Console.WriteLine ("Elapsed time rotation {0}", sw.ElapsedMilliseconds);
			sw.Restart ();

			bytesAfter = DecodeEncode(bytesAfter);

			sw.Stop ();
			Console.WriteLine ("Elapsed time {0}", sw.ElapsedMilliseconds);
			Console.WriteLine("Size before / after: {0} {1}", bytes.Length, bytesAfter.Length);
			System.IO.File.WriteAllBytes("test.jpg", bytes);

			Console.ReadLine ();
		}

		public static byte[] DecodeEncode(byte[] jpeg) 
		{
			uint width = 0;
			uint height = 0;
			var bytesRgb = mozjpeg.net.Compression.DecodeToRGB(jpeg, out width, out height);
			return mozjpeg.net.Compression.EncodeFromRGB(bytesRgb, width, height, 70, true);
		}
	}
}
