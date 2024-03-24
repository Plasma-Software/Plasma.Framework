using Plasma.Bytes.Access.Reading;
using Plasma.Bytes.Access.Writing;
using Plasma.Bytes.Codec;

namespace Plasma.Bytes.Wrapper
{
    public class ByteCodec<T>(IByteWrapper wrapper, ICodec<T> codec, int index = 0) : IByteCodec<T>
    {
        public IByteWrapper Wrapper { get; } = wrapper;
        public ICodec<T> Codec { get; } = codec;
        public int Index { get; protected set; } = index;
        
        public IByteCodec<T> Write(T value)
        {
            WriteResult result = Codec.Encode(value, Wrapper.Writer(Index));
            
            Index += result.Index;
            
            return this;
        }

        public T Read()
        {
            ReadResult<T> result = Codec.Decode(Wrapper.Reader(Index));
            
            Index += result.Index;
            
            return result.Value;
        }

        public IByteCodec<T> Reset()
        {
            Index = 0;
            
            return this;
        }

        public IByteWrapper Finish() => Wrapper;
    }
}