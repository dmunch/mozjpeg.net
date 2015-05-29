### Don't forget to download 	gas-preprocessor
### http://sourceforge.net/p/libjpeg-turbo/code/HEAD/tree/gas-preprocessor/gas-preprocessor.pl?format=raw

pushd `dirname $0` > /dev/null
BASEDIR=`pwd`
popd > /dev/null

#BASEDIR=$(dirname $0)
BINPATH=$BASEDIR/../bin
SRCPATH=$BASEDIR/../mozjpeg

mkdir -p $BINPATH/mozjpeg-ios-i386/ && cd $BINPATH/mozjpeg-ios-i386
IOS_GCC=/Applications/Xcode.app/Contents/Developer/Toolchains/XcodeDefault.xctoolchain/usr/bin/clang
IOS_PLATFORMDIR=/Applications/Xcode.app/Contents/Developer/Platforms/iPhoneSimulator.platform/
IOS_SYSROOT=$IOS_PLATFORMDIR/Developer/SDKs/iPhoneSimulator.sdk
IOS_CFLAGS="-arch i386 -miphoneos-version-min=7"
$SRCPATH/configure  --host i386-apple-darwin10 --enable-shared=false    CC="$IOS_GCC" LD="$IOS_GCC"     CFLAGS="-mfloat-abi=softfp -isysroot $IOS_SYSROOT -O3 $IOS_CFLAGS"     LDFLAGS="-mfloat-abi=softfp -isysroot $IOS_SYSROOT $IOS_CFLAGS"     CCASFLAGS="-no-integrated-as $IOS_CFLAGS"
make -j 4

mkdir -p $BINPATH/mozjpeg-ios-x86_64/ && cd $BINPATH/mozjpeg-ios-x86_64
IOS_GCC=/Applications/Xcode.app/Contents/Developer/Toolchains/XcodeDefault.xctoolchain/usr/bin/clang
IOS_PLATFORMDIR=/Applications/Xcode.app/Contents/Developer/Platforms/iPhoneSimulator.platform/
IOS_SYSROOT=$IOS_PLATFORMDIR/Developer/SDKs/iPhoneSimulator.sdk
IOS_CFLAGS="-arch x86_64 -miphoneos-version-min=7"
$SRCPATH/configure  --host x86_64-apple-darwin10 --enable-shared=false    CC="$IOS_GCC" LD="$IOS_GCC"     CFLAGS="-mfloat-abi=softfp -isysroot $IOS_SYSROOT -O3 $IOS_CFLAGS"     LDFLAGS="-mfloat-abi=softfp -isysroot $IOS_SYSROOT $IOS_CFLAGS"     CCASFLAGS="-no-integrated-as $IOS_CFLAGS"
make -j 4

mkdir -p $BINPATH/mozjpeg-ios-arm64/ && cd $BINPATH/mozjpeg-ios-arm64
IOS_PLATFORMDIR=/Applications/Xcode.app/Contents/Developer/Platforms/iPhoneOS.platform
IOS_SYSROOT=$IOS_PLATFORMDIR/Developer/SDKs/iPhoneOS.sdk
IOS_GCC=/Applications/Xcode.app/Contents/Developer/Toolchains/XcodeDefault.xctoolchain/usr/bin/clang
IOS_CFLAGS="-arch arm64 -miphoneos-version-min=7"

$SRCPATH/configure  --host  aarch64-apple-darwin --enable-shared=false    CC="$IOS_GCC" LD="$IOS_GCC"     CFLAGS="-mfloat-abi=softfp -isysroot $IOS_SYSROOT -O3 $IOS_CFLAGS"     LDFLAGS="-mfloat-abi=softfp -isysroot $IOS_SYSROOT $IOS_CFLAGS"     CCASFLAGS="-no-integrated-as $IOS_CFLAGS"
make -j 4

mkdir -p $BINPATH/mozjpeg-ios-armv7/ && cd $BINPATH/mozjpeg-ios-armv7
IOS_CFLAGS="-arch armv7  -miphoneos-version-min=7"
$SRCPATH/configure  --host  arm-apple-darwin10 --enable-shared=false    CC="$IOS_GCC" LD="$IOS_GCC"     CFLAGS="-mfloat-abi=softfp -isysroot $IOS_SYSROOT -O3 $IOS_CFLAGS"     LDFLAGS="-mfloat-abi=softfp -isysroot $IOS_SYSROOT $IOS_CFLAGS"     CCASFLAGS="-no-integrated-as $IOS_CFLAGS"
make -j 4

mkdir -p $BINPATH/mozjpeg-ios-armv7s/ && cd $BINPATH/mozjpeg-ios-armv7s
IOS_CFLAGS="-arch armv7s  -miphoneos-version-min=7"
$SRCPATH/configure  --host  arm-apple-darwin10 --enable-shared=false    CC="$IOS_GCC" LD="$IOS_GCC"     CFLAGS="-mfloat-abi=softfp -isysroot $IOS_SYSROOT -O3 $IOS_CFLAGS"     LDFLAGS="-mfloat-abi=softfp -isysroot $IOS_SYSROOT $IOS_CFLAGS"     CCASFLAGS="-no-integrated-as $IOS_CFLAGS"

make -j 4

lipo -create -output $BINPATH/libmozjpeg.a  $BINPATH/mozjpeg-ios-i386/.libs/libjpeg.a $BINPATH/mozjpeg-ios-x86_64/.libs/libjpeg.a $BINPATH/mozjpeg-ios-armv7s/.libs/libjpeg.a $BINPATH/mozjpeg-ios-armv7/.libs/libjpeg.a $BINPATH/mozjpeg-ios-arm64/.libs/libjpeg.a
