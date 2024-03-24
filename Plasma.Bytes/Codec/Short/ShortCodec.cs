using System.Buffers;
using Plasma.Bytes.Access.Reading;
using Plasma.Bytes.Access.Writing;
using Plasma.Bytes.Codec.Decoding;
using Plasma.Bytes.Codec.Encoding;

namespace Plasma.Bytes.Codec.Short
{
    public class ShortCodec : ICodec<short>
    {
        public IEncoder<short> Encoder => new Codec.Encoder();
        public IDecoder<short> Decoder => new Codec.Decoder();

        public class Codec
        {
            public class Encoder : IEncoder<short>
            {
                public WriteResult Encode(short value, IByteWriter writer)
                {
                    foreach (byte b in NumericHelper.Encode(value))
                    {
                        writer.Write(b);
                    }
                    
                    return writer.Finish();
                }
            }

            public class Decoder : IDecoder<short>
            {
                public ReadResult<short> Decode(IByteReader reader)
                {
                    byte[] bytes = reader.ReadMany(2);
            
                    short value = (short) NumericHelper.Decode(bytes);
                    
                    return reader.Finish(value);
                }
            }
        }        
    }
}