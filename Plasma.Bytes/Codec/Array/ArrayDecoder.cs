using Plasma.Bytes.Access.Reading;
using Plasma.Bytes.Codec.Decoding;

namespace Plasma.Bytes.Codec.Array
{
    public class ArrayDecoder<T>(ICodec<T> codec, int length) : IDecoder<T[]> {
        public ReadResult<T[]> Decode(IByteReader reader)
        {
            T[] array = new T[length];
            
            for (int i = 0; i < length; i++)
            {
                array[i] = codec.Decode(reader).Value;
            }
            
            return reader.Finish(array);
        }
    }
}