using Plasma.Bytes.Access.Writing;
using Plasma.Bytes.Codec.Decoding;

namespace Plasma.Bytes.Codec.Encoding
{
    public interface IEncoder<in T>
    {
        WriteResult Encode(T value, IByteWriter writer);
    }
}