using Plasma.Bytes.Access.Reading;
using Plasma.Bytes.Access.Writing;
using Plasma.Bytes.Codec.Decoding;
using Plasma.Bytes.Codec.Encoding;

namespace Plasma.Bytes.Codec.Float
{
    public class FloatCodec : ICodec<float>
    {
        public IEncoder<float> Encoder => new Codec.Encoder();
        public IDecoder<float> Decoder => new Codec.Decoder();

        public class Codec
        {
            public class Encoder : IEncoder<float>
            {
                public WriteResult Encode(float value, IByteWriter writer)
                {
                    byte[] bytes = BitConverter.GetBytes(value);
                    foreach (byte b in bytes)
                    {
                        writer.Write(b);
                    }

                    return writer.Finish();
                }
            }

            public class Decoder : IDecoder<float>
            {
                public ReadResult<float> Decode(IByteReader reader)
                {
                    byte[] bytes = reader.ReadMany(4);
                    float value = BitConverter.ToSingle(bytes, 0);
                    
                    return reader.Finish(value);
                }
            }
        }        
    }
}