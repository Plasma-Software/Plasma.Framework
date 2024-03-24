using Plasma.Bytes.Codec.Decoding;
using Plasma.Bytes.Codec.Encoding;

namespace Plasma.Bytes.Codec.VarInt
{
    public class VarIntCodec : ICodec<int>
    {
        public IEncoder<int> Encoder => new VarIntEncoder();
        public IDecoder<int> Decoder => new VarIntDecoder();
    }
}