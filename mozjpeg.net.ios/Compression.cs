using CoreGraphics;
using UIKit;

namespace mozjpeg.net
{
	public partial class Compression
	{
		public static UIImage DecodeToUImage(byte[] jpeg) 
		{
			uint width = 0;
			uint height = 0;
			var rgbBytes = mozjpeg.net.Compression.DecodeToRGB (jpeg, out width, out height);
			using (var provider = new CGDataProvider (rgbBytes, 0, rgbBytes.Length))
			using (var colorSpace = CGColorSpace.CreateDeviceRGB ())
			using (var cgImage = new CGImage ((int)width, (int)height, 8, 24, (int)(3 * width), colorSpace, CGImageAlphaInfo.None, provider, null, false, CGColorRenderingIntent.Default))
			{
				return new UIImage (cgImage);
			}
		}
	}
}