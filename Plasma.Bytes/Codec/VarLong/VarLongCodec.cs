using Plasma.Bytes.Codec.Decoding;
using Plasma.Bytes.Codec.Encoding;

namespace Plasma.Bytes.Codec.VarLong
{
    public class VarLongCodec : ICodec<long>
    {
        public IEncoder<long> Encoder => new VarLongEncoder();
        public IDecoder<long> Decoder => new VarLongDecoder();
    }
}