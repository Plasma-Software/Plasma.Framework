using Plasma.Buffer.Mutable;

namespace Plasma.Bytes.Buffer
{
    public class ByteBuffer(int capacity) : Buffer<byte>(capacity), IByteBuffer;
}