using Plasma.Bytes.Access.Writing;
using Plasma.Bytes.Codec.Encoding;

namespace Plasma.Bytes.Codec.VarLong
{
    public class VarLongEncoder : IEncoder<long>
    {
        public WriteResult Encode(long value, IByteWriter writer)
        {
            while (true)
            {
                if ((value & ~Codec.SEGMENT_BITS) == 0)
                {
                    return writer
                        .Write((byte) value)
                        .Finish();
                }

                writer.Write((byte) ((value & (long) Codec.SEGMENT_BITS) | (long) Codec.CONTINUE_BIT));
                
                value >>>= 7;
            }
        }
    }
}