namespace Plasma.Buffer.Mutable
{
    public class Buffer<T> : AbstractBuffer<T>
    {
        public Buffer(int capacity) : base(capacity) { }
        public Buffer(T[] buffer) : base(buffer.Length) { }
        public Buffer(IEnumerable<T> buffer) : base(buffer) { } 
    }
}