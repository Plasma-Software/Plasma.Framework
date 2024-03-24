namespace Plasma.Buffer.Immutable
{
    public abstract partial class AbstractReadOnlyBuffer<T>
    {
        internal class Messages
        {
            internal static string IndexOutOfBounds(IReadOnlyBuffer<T> buffer, int index = 0)
                => $"Index {index} was out of bounds for size {buffer.Size}";
        }
    }
}