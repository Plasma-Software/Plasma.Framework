using Plasma.Bytes.Codec.Decoding;
using Plasma.Bytes.Codec.Encoding;

namespace Plasma.Bytes.Codec.Array
{
    public class ArrayCodec<T>(ICodec<T> codec, int length) : ICodec<T[]>
    {
        public IEncoder<T[]> Encoder => new ArrayEncoder<T>(codec);
        public IDecoder<T[]> Decoder => new ArrayDecoder<T>(codec, length);
    }
}