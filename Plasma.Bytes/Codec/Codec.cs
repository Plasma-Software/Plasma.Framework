using Plasma.Bytes.Codec.Array;
using Plasma.Bytes.Codec.Boolean;
using Plasma.Bytes.Codec.Byte;
using Plasma.Bytes.Codec.Double;
using Plasma.Bytes.Codec.Float;
using Plasma.Bytes.Codec.Int;
using Plasma.Bytes.Codec.Long;
using Plasma.Bytes.Codec.Short;
using Plasma.Bytes.Codec.String;
using Plasma.Bytes.Codec.VarInt;
using Plasma.Bytes.Codec.VarLong;

namespace Plasma.Bytes.Codec
{
    public static class Codec
    {
        public static readonly int SEGMENT_BITS = 0x7F;
        public static readonly int CONTINUE_BIT = 0x80;
        
        public static class Variable
        {
            public static ICodec<int> VarInt() => new VarIntCodec();
            public static ICodec<long> VarLong() => new VarLongCodec();
        }
        
        public static class Primitive
        {
            public static ICodec<string> String(int size = 32767) => new StringCodec(size);
            public static ICodec<bool> Boolean() => new BooleanCodec();
            public static ICodec<sbyte> Byte() => new ByteCodec();
            public static ICodec<byte> UnsignedByte() => new UnsignedByteCodec();
            public static ICodec<short> Short() => new ShortCodec();
            public static ICodec<ushort> UnsignedShort() => new UnsignedShortCodec();
            public static ICodec<int> Int() => new IntCodec();
            public static ICodec<long> Long() => new LongCodec();
            public static ICodec<float> Float() => new FloatCodec();
            public static ICodec<double> Double() => new DoubleCodec();
            
            public static ICodec<T[]> Array<T>(ICodec<T> codec, int length) => new ArrayCodec<T>(codec, length);
        }
    }
}