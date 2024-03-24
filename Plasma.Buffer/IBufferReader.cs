namespace Plasma.Buffer
{
    public interface IBufferReader<T>
        : IBufferAccessor<T, IBufferReader<T>, IReadOnlyBuffer<T>>
    {
        // I'm not sure what other methods we need in here, but alright.
        T Read();
        
        // wtf is this name.
        ICollection<T> Collect(int length);
    }
}