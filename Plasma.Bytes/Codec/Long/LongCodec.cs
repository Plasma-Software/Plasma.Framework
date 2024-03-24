using System.Buffers;
using Plasma.Bytes.Access.Reading;
using Plasma.Bytes.Access.Writing;
using Plasma.Bytes.Codec.Decoding;
using Plasma.Bytes.Codec.Encoding;

namespace Plasma.Bytes.Codec.Long
{
    public class LongCodec : ICodec<long>
    {
        public IEncoder<long> Encoder => new Codec.Encoder();
        public IDecoder<long> Decoder => new Codec.Decoder();

        public class Codec
        {
            public class Encoder : IEncoder<long>
            {
                public WriteResult Encode(long value, IByteWriter writer)
                {
                    foreach (byte b in NumericHelper.Encode(value))
                    {
                        writer.Write(b);
                    }

                    return writer.Finish();
                }
            }

            public class Decoder : IDecoder<long>
            {
                public ReadResult<long> Decode(IByteReader reader)
                {
                    byte[] bytes = reader.ReadMany(8);
            
                    long value = (long) NumericHelper.Decode(bytes);
                    
                    return reader.Finish(value);
                }
            }
        }        
    }
}