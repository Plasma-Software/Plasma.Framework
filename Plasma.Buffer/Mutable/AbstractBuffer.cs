using Plasma.Buffer.Immutable;

namespace Plasma.Buffer.Mutable
{
    public abstract class AbstractBuffer<T> : AbstractReadOnlyBuffer<T>, IBuffer<T>
    {
        protected AbstractBuffer(int capacity) : base(capacity) { }
        protected AbstractBuffer(T[] buffer) : base(buffer) { }
        protected AbstractBuffer(IEnumerable<T> buffer) : base(buffer) { }
        
        public IBuffer<T> Write(int index, T element)
        {
            Buffer[index] = element;

            return this;
        }

        public IBuffer<T> Write(T element) => Write(0, element);

        public IBuffer<T> WriteRange(int index, params T[] elements)
        {
            for (int i = index; i < elements.Length + index; i++)
            {
                Write(i, elements[i - index]);
            }

return this;
        }

        public IBuffer<T> WriteRange(int index, IEnumerable<T> elements) => WriteRange(index, elements.ToArray());

        public IBuffer<T> WriteRange(params T[] elements) => WriteRange(0, elements);

        public IBuffer<T> WriteRange(IEnumerable<T> elements) => WriteRange(0, elements);

        public IBufferWriter<T> Writer(int index = 0) => new BufferWriter<T>(this, index);

        public IBuffer<T> Remove(T element)
        {
            for (int i = 0; i < Size; i++)
            {
                if (EqualityComparer<T>.Default.Equals(Buffer[i], element))
                {
                    Buffer[i] = Empty;
                }
            }

            return this;
        }

        public IBuffer<T> RemoveRange(params T[] elements)
        {
            foreach (T element in elements)
            {
                Remove(element);
            }

            return this;
        }

        public IBuffer<T> RemoveRange(IEnumerable<T> elements)
        {
            foreach (T element in elements)
            {
                Remove(element);
            }

            return this;
        }

        public IBuffer<T> RemoveAt(int index)
        {
            if (index >= 0 && index < Size)
            {
                Buffer[index] = Empty;
            }

            return this;
        }

        public IBuffer<T> RemoveRangeAt(params int[] indices)
        {
            foreach (int index in indices)
            {
                RemoveAt(index);
            }

            return this;
        }

        public IBuffer<T> RemoveRangeAt(IEnumerable<int> indices)
        {
            foreach (int index in indices)
            {
                RemoveAt(index);
            }
            
            return this;
        }

        public IBuffer<T> RemoveRange(int min, int max)
        {
            if (min >= 0 && max >= min && max < Size)
            {
                for (int i = min; i <= max; i++)
                {
                    Buffer[i] = Empty;
                }
            }
            
            return this;
        }

        public IBuffer<T> Fill(T item)
        {
            for (int i = 0; i < Size; i++)
            {
                Buffer[i] = item;
            }
            
            return this;
        }

        public IBuffer<T> Fill(params T[] items)
        {
            for (int i = 0; i < items.Length; i++)
            {
                Buffer[i] = items[i];
            }
            
            return this;
        }

        public IBuffer<T> Mask(params int[] mask)
        {
            for (int i = 0; i < Size; i++)
            {
                if (!mask.Contains(i))
                {
                    Buffer[i] = Empty;
                }
            }

            return this;
        }

        public IBuffer<T> Mask(IEnumerable<int> mask) => Mask(mask.ToArray());

        public IBuffer<T> Mask(params byte[] mask)
        {
            for (int i = 0; i < Size; i++)
            {
                if (!mask.Contains((byte)i))
                {
                    Buffer[i] = Empty;
                }
            }
        
            return this;
        }
        
        public IBuffer<T> Mask(IEnumerable<byte> mask) => Mask(mask.ToArray());
        
        public IBuffer<T> Clear()
        {
            for (int i = 0; i < Size; i++)
            {
                Buffer[i] = Empty;
            }
        
            return this;
        }
    }
}