using Plasma.Bytes.Access.Reading;
using Plasma.Bytes.Access.Writing;
using Plasma.Bytes.Codec.Decoding;
using Plasma.Bytes.Codec.Encoding;

namespace Plasma.Bytes.Codec
{
    public interface ICodec<T>
    {
        public IEncoder<T> Encoder { get; }
        public IDecoder<T> Decoder { get; }
        
        public WriteResult Encode(T value, IByteWriter writer) => Encoder.Encode(value, writer);
        public ReadResult<T> Decode(IByteReader reader) => Decoder.Decode(reader);
    }
}