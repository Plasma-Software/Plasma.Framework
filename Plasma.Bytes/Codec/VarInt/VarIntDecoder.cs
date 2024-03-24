using Plasma.Bytes.Access.Reading;
using Plasma.Bytes.Codec.Decoding;

namespace Plasma.Bytes.Codec.VarInt
{
    public class VarIntDecoder : IDecoder<int>
    {
        public ReadResult<int> Decode(IByteReader reader)
        {
            int value = 0, position = 0; 
            
            while (true)
            {
                byte currentByte = reader.Read();
                
                value |= (currentByte & Codec.SEGMENT_BITS) << position;
                
                if ((currentByte & Codec.CONTINUE_BIT) == 0) break;
                
                position += 7;
                
                if (position >= 32) throw new Exception("VarInt is too big! should be maximum 32 bit.");
            }
            
            return reader.Finish(value);
        }
    }
}