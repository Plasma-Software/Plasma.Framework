using Plasma.Bytes.Access.Reading;

namespace Plasma.Bytes.Codec.Decoding
{
    public interface IDecoder<T>
    {
        ReadResult<T> Decode(IByteReader reader);
    }
}