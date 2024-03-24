using Plasma.Bytes.Access.Writing;
using Plasma.Bytes.Codec.Encoding;

namespace Plasma.Bytes.Codec.VarInt
{
    public class VarIntEncoder : IEncoder<int>
    {
        public WriteResult Encode(int value, IByteWriter writer)
        {
            while (true)
            {
                if ((value & ~Codec.SEGMENT_BITS) == 0)
                {
                    return writer
                        .Write((byte) value)
                        .Finish();
                }
                
                writer.Write((byte) ((value & Codec.SEGMENT_BITS) | Codec.CONTINUE_BIT));
                
                value >>>= 7;
            }
        }
    }
}