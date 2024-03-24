namespace Plasma.Buffer.Immutable
{
    public class ReadOnlyBuffer<T> : AbstractReadOnlyBuffer<T>
    {
        public ReadOnlyBuffer(int capacity) : base(capacity) { }
        public ReadOnlyBuffer(T[] buffer) : base(buffer.Length) { }
        public ReadOnlyBuffer(IEnumerable<T> buffer) : base(buffer) { } 
    }
}