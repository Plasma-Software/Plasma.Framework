namespace Plasma.Buffer
{
    public interface IBuffer<T> : IReadOnlyBuffer<T>
    {
        public static IBuffer<T> Alloc(int size) => BufferHelper.Allocate<T>(size);
        public static IBuffer<T> Alloc(T[] items) => BufferHelper.Allocate(items);
        public static IBuffer<T> Alloc(IEnumerable<T> items) => BufferHelper.Allocate(items);
        
        public static IReadOnlyBuffer<T> AllocImmutable(int size) => BufferHelper.AllocateImmutable<T>(size);
        public static IReadOnlyBuffer<T> AllocImmutable(T[] items) => BufferHelper.AllocateImmutable(items);
        public static IReadOnlyBuffer<T> AllocImmutable(IEnumerable<T> items) => BufferHelper.AllocateImmutable(items);
        
        public new T this[int index]
        {
            get => Read(index);
            set => Write(index, value);
        }
        
        IBuffer<T> Write(int index, T element);
        IBuffer<T> Write(T element);
        
        IBuffer<T> WriteRange(int index, params T[] elements);
        IBuffer<T> WriteRange(int index, IEnumerable<T> elements);

        IBuffer<T> WriteRange(params T[] elements);
        IBuffer<T> WriteRange(IEnumerable<T> elements);
        
        IBufferWriter<T> Writer(int index = 0);
        
        IBuffer<T> Remove(T element);
        IBuffer<T> RemoveRange(params T[] elements);
        IBuffer<T> RemoveRange(IEnumerable<T> elements);
        
        IBuffer<T> RemoveAt(int index);
        IBuffer<T> RemoveRangeAt(params int[] indices);
        IBuffer<T> RemoveRangeAt(IEnumerable<int> indices);
        
        IBuffer<T> RemoveRange(int min, int max);
        
        IBuffer<T> Fill(T item);
        IBuffer<T> Fill(params T[] items);
        
        IBuffer<T> Mask(params int[] mask);
        IBuffer<T> Mask(IEnumerable<int> mask);
        
        IBuffer<T> Mask(params byte[] mask);
        IBuffer<T> Mask(IEnumerable<byte> mask);
        
        IBuffer<T> Clear();
    }
}