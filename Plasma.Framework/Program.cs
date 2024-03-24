// See https://aka.ms/new-console-template for more information

using Plasma.Buffer;
using Plasma.Bytes.Codec;
using Plasma.Bytes.Wrapper;

IByteWrapper wrapper = IByteWrapper.Alloc(64);

IByteCodec<string> codec = wrapper.Codec(Codec.Primitive.String());

codec.Write("Hello, World!");
codec.Write("This is not funny!");
codec.Write("Okay bro!");

codec.Reset();

Console.WriteLine(wrapper.Buffer);

Console.WriteLine(codec.Read());
Console.WriteLine(codec.Read());
Console.WriteLine(codec.Read());