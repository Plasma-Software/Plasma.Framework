using Plasma.Bytes.Access.Reading;
using Plasma.Bytes.Access.Writing;
using Plasma.Bytes.Codec.Decoding;
using Plasma.Bytes.Codec.Encoding;

namespace Plasma.Bytes.Codec.Byte
{
    public class UnsignedByteCodec : ICodec<byte>
    {
        public IEncoder<byte> Encoder => new Codec.Encoder();
        public IDecoder<byte> Decoder => new Codec.Decoder();
        
        public class Codec
        {
            public class Encoder : IEncoder<byte>
            {
                public WriteResult Encode(byte value, IByteWriter writer) => writer.Write(value).Finish();
            }

            public class Decoder : IDecoder<byte>
            {
                public ReadResult<byte> Decode(IByteReader reader) => reader.Finish(reader.Read());
            }
        }
    }
}