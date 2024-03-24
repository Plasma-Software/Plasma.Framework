using Plasma.Bytes.Access.Reading;
using Plasma.Bytes.Access.Writing;
using Plasma.Bytes.Codec.Decoding;
using Plasma.Bytes.Codec.Encoding;

namespace Plasma.Bytes.Codec.Boolean
{
    public class BooleanCodec : ICodec<bool>
    {
        public IEncoder<bool> Encoder => new Codec.Encoder();
        public IDecoder<bool> Decoder => new Codec.Decoder();
        
        
        public class Codec {
            public static readonly byte TRUE = 0x01;
            public static readonly byte FALSE = 0x00;
            
            public class Encoder : IEncoder<bool>
            {
                public WriteResult Encode(bool value, IByteWriter writer) => writer.Write(value ? TRUE : FALSE).Finish();
            }
            
            public class Decoder : IDecoder<bool>
            {
                public ReadResult<bool> Decode(IByteReader reader)
                {
                    byte b = reader.Read();
                    
                    if (b == TRUE)
                    {
                        return reader.Finish(true);
                    }
                    else if (b == FALSE)
                    {
                        return reader.Finish(false);
                    }
                    
                    throw new Exception($"Unknown boolean value {b}");
                }
            }
        }
    }
}