using System.Buffers;
using Plasma.Bytes.Access.Reading;
using Plasma.Bytes.Access.Writing;
using Plasma.Bytes.Codec.Decoding;
using Plasma.Bytes.Codec.Encoding;

namespace Plasma.Bytes.Codec.Int
{
    public class IntCodec : ICodec<int>
    {
        public IEncoder<int> Encoder => new Codec.Encoder();
        public IDecoder<int> Decoder => new Codec.Decoder();

        public class Codec
        {
            public class Encoder : IEncoder<int>
            {
                public WriteResult Encode(int value, IByteWriter writer)
                {
                    foreach (byte b in NumericHelper.Encode(value))
                    {
                        writer.Write(b);
                    }

                    return writer.Finish();
                }
            }

            public class Decoder : IDecoder<int>
            {
                public ReadResult<int> Decode(IByteReader reader)
                {
                    byte[] bytes = reader.ReadMany(4);
            
                    int value = (int) NumericHelper.Decode(bytes);
                    
                    return reader.Finish(value);
                }
            }
        }        
    }
}