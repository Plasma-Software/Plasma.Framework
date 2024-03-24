namespace Plasma.Bytes.Access.Reading
{
    public class ReadResult<T>(T value, int index)
    {
        public T Value { get; } = value;
        public int Index { get; } = index;
    }
}