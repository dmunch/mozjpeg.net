
pushd `dirname $0` > /dev/null
BASEDIR=`pwd`
popd > /dev/null

BINPATH=$BASEDIR/../bin
SRCPATH=$BASEDIR/../../mozjpeg

function build {

	TOOLCHAIN=${NDK_PATH}/toolchains/${HOSTSHORT}-${TOOLCHAIN_VERSION}/prebuilt/${BUILD_PLATFORM}
	SYSROOT=${NDK_PATH}/platforms/android-${ANDROID_VERSION}/arch-$ARCH2
	ANDROID_INCLUDES="-I${SYSROOT}/usr/include -I${TOOLCHAIN}/include"
	ANDROID_CFLAGS="$ARCH -fprefetch-loop-arrays -fstrict-aliasing --sysroot=${SYSROOT}"

	CXX=${TOOLCHAIN}/bin/${HOST}-g++
	ANDROID_LIBS="-L${SYSROOT}/usr/lib -I${TOOLCHAIN}/lib"


	export CPP=${TOOLCHAIN}/bin/${HOST}-cpp
	export AR=${TOOLCHAIN}/bin/${HOST}-ar
	export AS=${TOOLCHAIN}/bin/${HOST}-as
	export NM=${TOOLCHAIN}/bin/${HOST}-nm
	export CC=${TOOLCHAIN}/bin/${HOST}-gcc
	export LD=${TOOLCHAIN}/bin/${HOST}-ld
	export RANLIB=${TOOLCHAIN}/bin/${HOST}-ranlib
	export OBJDUMP=${TOOLCHAIN}/bin/${HOST}-objdump
	export STRIP=${TOOLCHAIN}/bin/${HOST}-strip

	mkdir -p $BINPATH/mozjpeg-android-$DIR/ && cd $BINPATH/mozjpeg-android-$DIR

	$SRCPATH/configure --host=${HOST} \
	CFLAGS="${ANDROID_INCLUDES} ${ANDROID_CFLAGS} -O3" \
	CPPFLAGS="${ANDROID_INCLUDES} ${ANDROID_CFLAGS}" \
	LDFLAGS="${ANDROID_LIBS} ${ANDROID_CFLAGS}" --with-simd ${1+"$@"} 

	make -j 4
	cp .libs/libjpeg.so libmozjpeg.so
}

NDK_PATH=/Users/jack/Downloads/android-ndk-r10e
BUILD_PLATFORM=darwin-x86_64
TOOLCHAIN_VERSION=4.9
ANDROID_VERSION=19

HOSTSHORT=x86
HOST=i686-linux-android
ARCH="-march=i686" 
ARCH2=x86
DIR=x86

build

HOST=arm-linux-androideabi
HOSTSHORT=$HOST
ARCH="-march=armv6 -mfloat-abi=softfp"
ARCH2=arm
DIR=armv6

build

HOST=arm-linux-androideabi
HOSTSHORT=$HOST
ARCH="-march=armv7-a -mfloat-abi=softfp"
ARCH2=arm
DIR=armv7-a

#build
