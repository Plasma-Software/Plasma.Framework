using Plasma.Bytes.Access.Reading;
using Plasma.Bytes.Access.Writing;
using Plasma.Bytes.Buffer;
using Plasma.Bytes.Codec;

namespace Plasma.Bytes.Wrapper
{
    public class ByteWrapper(int size) : IByteWrapper
    {
        public int Size { get; } = size;
        public IByteBuffer Buffer { get; } = IByteBuffer.Alloc(size);

        public IByteCodec<T> Codec<T>(ICodec<T> codec, int index = 0)
            => new ByteCodec<T>(this, codec, index);

        public IByteWriter Writer(int index = 0)
            => new ByteWriter(Buffer, index);

        public IByteReader Reader(int index = 0)
            => new ByteReader(Buffer, index);
    }
}