namespace Plasma.Buffer
{
    public interface IBufferWriter<T> : IBufferAccessor<T, IBufferWriter<T>, IBuffer<T>>
    {
        IBufferWriter<T> Write(T value);
        IBufferWriter<T> WriteMany(params T[] values);
    }
}