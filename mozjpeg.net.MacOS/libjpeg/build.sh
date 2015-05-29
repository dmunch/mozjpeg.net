pushd `dirname $0` > /dev/null
BASEDIR=`pwd`
popd > /dev/null

#BASEDIR=$(dirname $0)
BINPATH=$BASEDIR/../bin
SRCPATH=$BASEDIR/../../mozjpeg

echo $BASEDIR
mkdir -p $BINPATH/mozjpeg-macosx-i386/ && cd $BINPATH/mozjpeg-macosx-i386

$SRCPATH/configure  --host i686-apple-darwin \
    CFLAGS='-mmacosx-version-min=10.5 -O3 -m32' \
    LDFLAGS='-mmacosx-version-min=10.5 -m32'

make -j 4
cp $BINPATH/mozjpeg-macosx-i386/.libs/libjpeg.dylib $BINPATH/libmozjpeg.dylib
