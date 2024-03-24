namespace Plasma.Buffer
{
    public class BufferWriter<T>(IBuffer<T> buffer, int index = 0) : IBufferWriter<T>
    {
        public IBuffer<T> Buffer { get; } = buffer;
        public int Index { get; protected set; } = index;

        public IBufferWriter<T> Write(T value)
        {
            Buffer[Index++] = value;
            
            return this;
        }

        public IBufferWriter<T> WriteMany(params T[] values)
        {
            foreach (T value in values)
            {
                Write(value);
            }

            return this;
        }
        

        public IBufferWriter<T> Access(Action<IBuffer<T>> action)
        {
            action(Buffer);
        
            return this;
        }
        
        public IBufferWriter<T> Reset()
        {
            Index = 0;
        
            return this;
        }
    }
}