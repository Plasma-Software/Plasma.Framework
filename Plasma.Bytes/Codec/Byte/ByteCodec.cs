using Plasma.Bytes.Access.Reading;
using Plasma.Bytes.Access.Writing;
using Plasma.Bytes.Codec.Decoding;
using Plasma.Bytes.Codec.Encoding;

namespace Plasma.Bytes.Codec.Byte
{
    public class ByteCodec : ICodec<sbyte>
    {
        public IEncoder<sbyte> Encoder => new Codec.Encoder();
        public IDecoder<sbyte> Decoder => new Codec.Decoder();
        
        public class Codec
        {
            public class Encoder : IEncoder<sbyte>
            {
                public WriteResult Encode(sbyte value, IByteWriter writer) => writer.Write((byte) value).Finish();
            }

            public class Decoder : IDecoder<sbyte>
            {
                public ReadResult<sbyte> Decode(IByteReader reader) => reader.Finish((sbyte) reader.Read());
            }
        }
    }
}