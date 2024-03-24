using Plasma.Bytes.Codec.Decoding;
using Plasma.Bytes.Codec.Encoding;

namespace Plasma.Bytes.Codec.String
{
    public class StringCodec(int size) : ICodec<string>
    {
        public IEncoder<string> Encoder => new StringEncoder(size);
        public IDecoder<string> Decoder => new StringDecoder(size);
    }
}