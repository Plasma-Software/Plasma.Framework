namespace Plasma.Buffer
{
    public interface IReadOnlyBuffer : IReadOnlyBuffer<object>;
    
    public interface IReadOnlyBuffer<T> : IEnumerable<T>
    {
        public T Empty { get; }
        public bool IsReadOnly { get; }
        public int Size { get; }
        
        public T this[int index] => Read(index);
        
        T Read(int index = 0);
        
        IReadOnlyBuffer<T> ReadRange(params int[] indices);
        IReadOnlyBuffer<T> ReadRange(IEnumerable<int> indices);

        IReadOnlyBuffer<T> ReadRange(int min, int max);
        
        IBufferReader<T> Reader(int index = 0);
        
        int IndexOf(T element);
        int LastIndexOf(T element);
        
        bool Contains(T element);

        bool IsEmpty();
        bool Exists(int index);
        
        IReadOnlyBuffer<T> Subsection(int splitIndex);
        IReadOnlyBuffer<T> Subsection(int index, int length);
        
        IReadOnlyBuffer<T> Supersection(int splitIndex);
        IReadOnlyBuffer<T> Supersection(int index, int length);
        
        IReadOnlyBuffer<T> Concat(params IReadOnlyBuffer<T>[] others);
        IReadOnlyBuffer<T> Concat(IEnumerable<IReadOnlyBuffer<T>> others);
        IReadOnlyBuffer<T> Concat(IReadOnlyBuffer<T> other);
        
        IReadOnlyBuffer<T> Merge(params IReadOnlyBuffer<T>[] others);
        IReadOnlyBuffer<T> Merge(IEnumerable<IReadOnlyBuffer<T>> others);
        IReadOnlyBuffer<T> Merge(IReadOnlyBuffer<T> other);
        
        IReadOnlyBuffer<T>[] Split(int index);
        IReadOnlyBuffer<T>[] Split(params int[] indices);
        IReadOnlyBuffer<T>[] Split(IEnumerable<int> indices);
        
        IReadOnlyBuffer<T>[] Split(T element);
        IReadOnlyBuffer<T>[] Split(params T[] elements);
        IReadOnlyBuffer<T>[] Split(IEnumerable<T> elements);
        
        IReadOnlyBuffer<T> Resize(int size, int index = 0);
        
        IReadOnlyBuffer<T> CopyTo(ref T[] array, int arrayIndex = 0);
        IReadOnlyBuffer<T> CopyTo(ref IBuffer<T> buffer, int bufferIndex = 0);
        
        int Capacity();
        
        IReadOnlyBuffer<T> Immutable();
        IBuffer<T> Mutable();
    }
}