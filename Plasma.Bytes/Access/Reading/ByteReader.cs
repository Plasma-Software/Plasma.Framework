using Plasma.Bytes.Buffer;

namespace Plasma.Bytes.Access.Reading
{
    public class ByteReader(IByteBuffer buffer, int index = 0) : IByteReader
    {
        public IByteBuffer Buffer { get; } = buffer;
        public int Index { get; private set; } = index;
        
        public byte Read() => Buffer[Index++];

        public byte[] ReadMany(int length)
        {
            byte[] result = new byte[length];
            
            for (int i = 0; i < length; i++)
            {
                result[i] = Read();
            }
            
            return result;
        }

        public IByteReader Reset()
        {
            Index = 0;
            
            return this;
        }

        public ReadResult<T> Finish<T>(T value)
            => new(value, Index);
    }
}