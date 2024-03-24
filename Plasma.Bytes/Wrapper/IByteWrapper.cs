using Plasma.Bytes.Access.Reading;
using Plasma.Bytes.Access.Writing;
using Plasma.Bytes.Buffer;
using Plasma.Bytes.Codec;

namespace Plasma.Bytes.Wrapper
{
    public interface IByteWrapper
    {
        public static IByteWrapper Alloc(int size) => new ByteWrapper(size);
        
        public int Size { get; }
        public IByteBuffer Buffer { get; }
        
        IByteCodec<T> Codec<T>(ICodec<T> codec, int index = 0);
        
        IByteWriter Writer(int index = 0);
        IByteReader Reader(int index = 0);
    }
}