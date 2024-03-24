using Plasma.Bytes.Codec;

namespace Plasma.Bytes.Wrapper
{
    public interface IByteCodec<T>
    {
        public IByteWrapper Wrapper { get; }
        public ICodec<T> Codec { get; }
        public int Index { get; }
        
        IByteCodec<T> Write(T value);
        
        T Read();
        
        IByteCodec<T> Reset();
        
        IByteWrapper Finish();
    }
}