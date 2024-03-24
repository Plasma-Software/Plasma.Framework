using Plasma.Bytes.Access.Reading;
using Plasma.Bytes.Codec.Decoding;

namespace Plasma.Bytes.Codec.VarLong
{
    public class VarLongDecoder : IDecoder<long>
    {
        public ReadResult<long> Decode(IByteReader reader)
        {
            long value = 0;
            int position = 0; 

            while (true)
            {
                byte currentByte = reader.Read();
            
                value |= (long) (currentByte & Codec.SEGMENT_BITS) << position;
                
                if ((currentByte & Codec.CONTINUE_BIT) == 0) break;
                
                position += 7;
            
                if (position >= 64) throw new Exception("VarLong is too big! should be maximum 64 bit.");
            }
            
            return reader.Finish(value);
        }
    }
}