namespace Plasma.Buffer
{
    public class BufferReader<T>(IReadOnlyBuffer<T> buffer, int index = 0) : IBufferReader<T>
    {
        public IReadOnlyBuffer<T> Buffer { get; } = buffer;
        public int Index { get; protected set; } = index;
        
        public T Read() => Buffer[Index++];

        public ICollection<T> Collect(int length)
        {
            ICollection<T> collection = new List<T>();
            
            for (int i = 0; i < length; i++)
            {
                collection.Add(Read());
            }
            
            return collection;
        }
        
        public IBufferReader<T> Access(Action<IReadOnlyBuffer<T>> action)
        {
            action(Buffer);
            
            return this;
        }

        public IBufferReader<T> Reset()
        {
            Index = 0;
            
            return this;
        }
    }
}