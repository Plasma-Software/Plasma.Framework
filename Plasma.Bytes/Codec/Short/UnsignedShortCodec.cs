using System.Buffers;
using Plasma.Bytes.Access.Reading;
using Plasma.Bytes.Access.Writing;
using Plasma.Bytes.Codec.Decoding;
using Plasma.Bytes.Codec.Encoding;

namespace Plasma.Bytes.Codec.Short
{
    public class UnsignedShortCodec : ICodec<ushort>
    {
        public IEncoder<ushort> Encoder => new Codec.Encoder();
        public IDecoder<ushort> Decoder => new Codec.Decoder();

        public class Codec
        {
            public class Encoder : IEncoder<ushort>
            {
                public WriteResult Encode(ushort value, IByteWriter writer)
                {
                    foreach (byte b in NumericHelper.Encode((UInt128) value))
                    {
                        writer.Write(b);
                    }
                    
                    return writer.Finish();
                }
            }

            public class Decoder : IDecoder<ushort>
            {
                public ReadResult<ushort> Decode(IByteReader reader)
                {
                    byte[] bytes = reader.ReadMany(2);
            
                    ushort value = (ushort) NumericHelper.Decode(bytes);
                    
                    return reader.Finish(value);
                }
            }
        }        
    }
}