namespace Plasma.Buffer
{
    public interface IBufferAccessor<T, out T1, out TB>
        where TB : IReadOnlyBuffer<T>
        where T1 : IBufferAccessor<T, T1, TB>
    {
        public TB Buffer { get; }
        public int Index { get; }
        
        T1 Access(Action<TB> action);
        T1 Reset();
    }
}