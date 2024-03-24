using Plasma.Buffer;

namespace Plasma.Bytes.Buffer
{
    public interface IByteBuffer : IBuffer<byte>
    {
        public new static IByteBuffer Alloc(int capacity) => new ByteBuffer(capacity);
    }
}