using Plasma.Bytes.Buffer;
using Plasma.Bytes.Wrapper;

namespace Plasma.Bytes.Access.Writing
{
    public class ByteWriter(IByteBuffer buffer, int index = 0) : IByteWriter
    {
        public IByteBuffer Buffer { get; } = buffer;
        public int Index { get; protected set; } = index;
        
        public IByteWriter Write(byte value)
        {
            Buffer[Index++] = value;
            
            return this;
        }

        public IByteWriter WriteMany(params byte[] values)
        {
            foreach (byte value in values)
            {
                Write(value);
            }
            
            return this;
        }

        public IByteWriter Reset()
        {
            Index = 0;
            
            return this;
        }

        public WriteResult Finish() => new(Index);
    }
}