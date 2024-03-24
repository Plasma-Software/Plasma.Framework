using Plasma.Bytes.Buffer;

namespace Plasma.Bytes.Access.Reading
{
    public interface IByteReader
    {
        public IByteBuffer Buffer { get; }
        public int Index { get; }
        
        byte Read();
        byte[] ReadMany(int length);
        
        IByteReader Reset();
        
        ReadResult<T> Finish<T>(T value);
    }
}