using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

using Foundation;
using UIKit;
using System.IO;
using CoreGraphics;

namespace mozjpeg.net.test.ios
{
	[Register ("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate
	{
		public override UIWindow Window {
			get;
			set;
		}
		public override bool FinishedLaunching (UIApplication application, NSDictionary options)
		{
			this.Window = new UIWindow (UIScreen.MainScreen.Bounds);
			this.Window.RootViewController = new UIViewController ();

			var imageView = new UIImageView (this.Window.Frame) { UserInteractionEnabled = true };
			var bytes = GetResource ("mozjpeg.net.test.ios.testimage.jpg");

			var tapGesture = new UITapGestureRecognizer (() => {
				System.Diagnostics.Debug.WriteLine("tapped");
				bytes = mozjpeg.net.Transformation.Rotate (bytes);
				imageView.Image = new UIImage (NSData.FromArray (bytes));
			});
			imageView.AddGestureRecognizer (tapGesture);


			//imageView.Image = new UIImage (NSData.FromArray (bytes));
			imageView.Image = mozjpeg.net.Compression.DecodeToUImage(bytes);

			this.Window.RootViewController.Add (imageView);
			this.Window.MakeKeyAndVisible ();


			return true;
		}

		public byte[] GetResource(string resourceUrl)
		{
			using (var imageStream = typeof(AppDelegate).GetTypeInfo().Assembly.GetManifestResourceStream(resourceUrl))
			{
				using (var memStream = new MemoryStream())
				{
					imageStream.CopyTo(memStream);
					return memStream.ToArray();
				}
			}
		}

		// This method is invoked when the application is about to move from active to inactive state.
		// OpenGL applications should use this method to pause.
		public override void OnResignActivation (UIApplication application)
		{
		}
		
		// This method should be used to release shared resources and it should store the application state.
		// If your application supports background exection this method is called instead of WillTerminate
		// when the user quits.
		public override void DidEnterBackground (UIApplication application)
		{
		}
		
		// This method is called as part of the transiton from background to active state.
		public override void WillEnterForeground (UIApplication application)
		{
		}
		
		// This method is called when the application is about to terminate. Save data, if needed.
		public override void WillTerminate (UIApplication application)
		{
		}
	}
}

