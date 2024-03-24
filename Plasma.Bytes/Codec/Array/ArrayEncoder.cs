using Plasma.Bytes.Access.Writing;
using Plasma.Bytes.Codec.Encoding;

namespace Plasma.Bytes.Codec.Array
{
    public class ArrayEncoder<T>(ICodec<T> codec) : IEncoder<T[]>
    {
        public WriteResult Encode(T[] value, IByteWriter writer)
        {
            foreach (T v in value)
            {
                codec.Encode(v, writer);
            }
            
            return writer.Finish();
        }
    }
}