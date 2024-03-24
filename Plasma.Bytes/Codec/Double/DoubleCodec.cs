using Plasma.Bytes.Access.Reading;
using Plasma.Bytes.Access.Writing;
using Plasma.Bytes.Codec.Decoding;
using Plasma.Bytes.Codec.Encoding;

namespace Plasma.Bytes.Codec.Double
{
    public class DoubleCodec : ICodec<double>
    {
        public IEncoder<double> Encoder => new Codec.Encoder();
        public IDecoder<double> Decoder => new Codec.Decoder();

        public class Codec
        {
            public class Encoder : IEncoder<double>
            {
                public WriteResult Encode(double value, IByteWriter writer)
                {
                    byte[] bytes = BitConverter.GetBytes(value);
                    foreach (byte b in bytes)
                    {
                        writer.Write(b);
                    }

                    return writer.Finish();
                }
            }

            public class Decoder : IDecoder<double>
            {
                public ReadResult<double> Decode(IByteReader reader)
                {
                    byte[] bytes = reader.ReadMany(8);
                    double value = BitConverter.ToDouble(bytes, 0);

                    return reader.Finish(value);
                }
            }
        }        
    }
}

