using System;
using System.Reflection;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.IO;
using Android.Graphics;

namespace mozjpeg.net.droid.test
{
	[Activity (Label = "mozjpeg.net.droid.test", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		int count = 1;

		protected override void OnCreate (Bundle bundle)
		{
			var jpegBytes = GetResource ("mozjpeg.net.droid.test.testimage.jpg");

			base.OnCreate (bundle);

			var imageView = new ImageView(this);
			SetContentView (imageView);

			Bitmap bmp = BitmapFactory.DecodeByteArray (jpegBytes, 0, jpegBytes.Length);
			uint width = 0;
			uint height = 0;
			var bytes = mozjpeg.net.Compression.DecodeToRGB (jpegBytes, out width, out height);

			var stream = new System.IO.MemoryStream ();

			var sw = new System.Diagnostics.Stopwatch ();
			sw.Start ();
			bmp.Compress (Bitmap.CompressFormat.Jpeg, 100, stream);
			System.Diagnostics.Debug.WriteLine ("Android compression: {0}", sw.Elapsed);
			var bytesC1 = stream.ToArray ();

			sw.Restart ();
			var bytesC2 = mozjpeg.net.Compression.EncodeFromRGB (bytes, (uint)bmp.Width, (uint)bmp.Height, 100, false);
			System.Diagnostics.Debug.WriteLine ("jpeg-turbo compression: {0}", sw.Elapsed);

			sw.Restart ();
			var bytesC3 = mozjpeg.net.Compression.EncodeFromRGB (bytes, (uint)bmp.Width, (uint)bmp.Height, 100, true);
			System.Diagnostics.Debug.WriteLine ("mozjpeg compression: {0}", sw.Elapsed);
			sw.Stop ();

			imageView.SetImageBitmap (bmp);

			jpegBytes = bytesC3;
			imageView.Click += delegate {

				if(bmp != null) {
					bmp.Dispose();
					bmp = null;
				}
				sw.Restart();
				jpegBytes = mozjpeg.net.Transformation.Rotate (jpegBytes);
				sw.Stop();
				System.Diagnostics.Debug.WriteLine ("rotation: {0}", sw.Elapsed);

				bmp = BitmapFactory.DecodeByteArray (jpegBytes, 0, jpegBytes.Length);
				imageView.SetImageBitmap (bmp);		

			};
		}

		public byte[] GetResource(string resourceUrl)
		{
			using (var imageStream = typeof(MainActivity).GetTypeInfo().Assembly.GetManifestResourceStream(resourceUrl))
			{
				using (var memStream = new MemoryStream())
				{
					imageStream.CopyTo(memStream);
					return memStream.ToArray();
				}
			}
		}
	}
}


