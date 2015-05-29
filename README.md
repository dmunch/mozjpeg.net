# mozjpeg.net

P/Invoke wrappers around Mozilla's [mozjpeg](https://github.com/mozilla/mozjpeg) for .NET and Xamarin.

Exposes the complete libjpeg API in order to be able to control all the parameters of JPEG encoding and decoding. Also includes
lossless transformation methods.

The wrappers can be used with any version compatible to libjpeg ABI version 6.2, 
like libjpeg itself or [libjpeg-turbo](http://www.libjpeg-turbo.org). To distinguish the project from [libjpeg.net](https://github.com/BitMiracle/libjpeg.net), 
a managed implementation of libjpeg in C#, I decided to call this project mozjpeg.net. 
This also reflects the fact that the prebuilt NuGet packages for monoandroid and Xamarin.iOS ship with mozjpeg.

#### Installing the NuGet on Xamarin.iOS
The NuGet for Xamarin.iOS adds the static library libmozjpeg.a to your project root which contains the 
static libraries for i386, x32_64, ARMv7, ARMv7s and ARM64. Following the Xamarin documentation for [linking native libraries](http://developer.xamarin.com/guides/ios/advanced_topics/native_interop/) 
you have to configure Xamarin.iOS:

> To Configure Xamarin.iOS To Link the Library, on the project options for your final executable (not the library itself, but the final program) you need to add in "iOS Build"'s Extra argument (these are part of your project options) the "-gcc_flags" option followed by a quoted string that contains > all the extra libraries that are required for your program, for example:

> `-gcc_flags "-L${ProjectDir} -lMylibrary -force_load ${ProjectDir}/libMyLibrary.a"`

In our case the exact line equals to

	-gcc_flags "-L${ProjectDir} -lmozjpeg -force_load ${ProjectDir}/libmozjpeg.a"

I tried to automate this step by importing a `.target` file in the target project file, however
it doesn't seem to be supported:  https://forums.xamarin.com/discussion/42372/msproject-files-import-targets-file-with-mtouchextraargs