using System.Collections;
using System.Text;

namespace Plasma.Buffer.Immutable
{
    public abstract partial class AbstractReadOnlyBuffer<T>(int capacity) : IReadOnlyBuffer<T>
    {
        public virtual T Empty => default!;
        public virtual bool IsReadOnly => true;
        public int Size { get; } = capacity;
        
        protected T[] Buffer = new T[capacity];

        public T this[int index] => Read(index);
        
        protected AbstractReadOnlyBuffer(T[] buffer) : this(buffer.Length)
        {
            for (int i = 0; i < Size; i++)
            {
                Buffer[i] = buffer[i];
            }
        }
        
        protected AbstractReadOnlyBuffer(IEnumerable<T> buffer) : this(buffer.ToArray()) { } 

        public T Read(int index = 0)
        {
            CheckIndex(index);
            
            return Buffer[index];
        }

        public IReadOnlyBuffer<T> ReadRange(params int[] indices)
        {
            T[] buffer = new T[indices.Length];
            
            for (int i = 0; i < indices.Length; i++)
            {
                buffer[i] = Buffer[indices[i]];
            }
            
            return BufferHelper.AllocateImmutable(buffer);
        }

        public IReadOnlyBuffer<T> ReadRange(IEnumerable<int> indices) => ReadRange(indices.ToArray());

        public IReadOnlyBuffer<T> ReadRange(int min, int max)
        {
            T[] buffer = new T[max - min];
            
            for (int i = min; i < max; i++)
            {
                buffer[i - min] = Buffer[i];
            }
            
            return BufferHelper.AllocateImmutable(buffer);
        }

        public IBufferReader<T> Reader(int index = 0) => new BufferReader<T>(this, index);

        public int IndexOf(T element)
        {
            for (int i = 0; i < Size; i++)
            {
                T source = Buffer[i];
                
                if (EqualityComparer<T>.Default.Equals(source, element))
                {
                    return i;
                }
            }
            
            return -1;
        }

        public int LastIndexOf(T element)
        {
            int index = -1;
            
            for (int i = 0; i < Size; i++)
            {
                T source = Buffer[i];
                
                if (EqualityComparer<T>.Default.Equals(source, element))
                {
                    index = i;
                }
            }
            
            return index;
        }

        public bool Contains(T element) => IndexOf(element) != -1;

        public bool IsEmpty() => LastIndexOf(Empty) == -1;

        public bool Exists(int index) => !EqualityComparer<T>.Default.Equals(Buffer[index], Empty);

        public IReadOnlyBuffer<T> Subsection(int splitIndex)
        {
            CheckIndex(splitIndex);
            
            T[] buffer = new T[splitIndex];
            
            for (int i = 0; i < splitIndex; i++)
            {
                buffer[i] = Buffer[i];
            }
            
            return BufferHelper.AllocateImmutable(buffer);
        }

        public IReadOnlyBuffer<T> Subsection(int index, int length)
        {
            CheckIndex(index);
            CheckIndex(index + length);
            
            T[] buffer = new T[length];
            
            for (int i = index; i < index + length; i++)
            {
                buffer[i - index] = Buffer[i];
            }
            
            return BufferHelper.AllocateImmutable(buffer);
        }

        public IReadOnlyBuffer<T> Supersection(int splitIndex)
        {
            CheckIndex(splitIndex);
            
            T[] buffer = new T[Size - splitIndex];
            
            for (int i = Size; i > splitIndex; i--)
            {
                buffer[i - Size] = Buffer[i];
            }
            
            return BufferHelper.AllocateImmutable(buffer);
        }

        public IReadOnlyBuffer<T> Supersection(int index, int length)
        {
            CheckIndex(index);
            CheckIndex(index - length);
            
            T[] buffer = new T[length];
            
            for (int i = index; i > index - length; i--)
            {
                buffer[i - index] = Buffer[i];
            }
            
            return BufferHelper.AllocateImmutable(buffer);
        }

        public IReadOnlyBuffer<T> Concat(params IReadOnlyBuffer<T>[] others)
            => BufferHelper.Concat(this, others);

        public IReadOnlyBuffer<T> Concat(IEnumerable<IReadOnlyBuffer<T>> others)
            => BufferHelper.Concat(this, others.ToArray());

        public IReadOnlyBuffer<T> Concat(IReadOnlyBuffer<T> other)
            => BufferHelper.Concat(this, other);

        public IReadOnlyBuffer<T> Merge(params IReadOnlyBuffer<T>[] others)
            => BufferHelper.Merge(this, others);
        
        public IReadOnlyBuffer<T> Merge(IEnumerable<IReadOnlyBuffer<T>> others)
            => BufferHelper.Merge(this, others.ToArray());
        
        public IReadOnlyBuffer<T> Merge(IReadOnlyBuffer<T> other)
            => BufferHelper.Merge(this, other);

        public IReadOnlyBuffer<T>[] Split(int index)
        {
            IReadOnlyBuffer<T>[] buffers = new IReadOnlyBuffer<T>[2];
            
            buffers[0] = Subsection(index);
            buffers[1] = Supersection(index);
            
            return buffers;
        }

        public IReadOnlyBuffer<T>[] Split(params int[] indices)
        {
            if (indices.Length == 0) return [];

            Array.Sort(indices);

            IReadOnlyBuffer<T>[] buffers = new IReadOnlyBuffer<T>[indices.Length + 1];

            int start = 0, bufferIndex = 0;
            
            foreach (int index in indices)
            {
                if (index > start)
                {
                    buffers[bufferIndex] = Subsection(start, index - start);
            
                    start = index;
                    bufferIndex++;
                }
            }
            
            if (start < Size)
            {
                buffers[bufferIndex] = Subsection(start, Size - start);
            }

            return buffers;
        }

        public IReadOnlyBuffer<T>[] Split(IEnumerable<int> indices)
            => Split(indices.ToArray());
        

        public IReadOnlyBuffer<T>[] Split(T element) => Split(IndexOf(element));
        public IReadOnlyBuffer<T>[] Split(params T[] elements)
        {
            int[] indices = new int[elements.Length];

            for (int i = 0; i < elements.Length; i++)
            {
                indices[i] = IndexOf(elements[i]);
            }
            
            return Split(indices);
        }

        public IReadOnlyBuffer<T>[] Split(IEnumerable<T> elements) => Split(elements.ToArray());

        public IReadOnlyBuffer<T> Resize(int size, int index = 0)
        {
            T[] buffer = new T[size];
            
            CopyTo(ref buffer, index);

            return BufferHelper.AllocateImmutable(buffer);
        }

        public IReadOnlyBuffer<T> CopyTo(ref T[] array, int arrayIndex = 0)
        {
            for (int i = 0; i < Size; i++)
            {
                array[i + arrayIndex] = Buffer[i];
            }
            
            return this;
        }

        public IReadOnlyBuffer<T> CopyTo(ref IBuffer<T> buffer, int bufferIndex = 0)
        {
            for (int i = 0; i < Size; i++)
            {
                buffer[i + bufferIndex] = Buffer[i];
            }
            
            return this;
        }
        
        protected internal void CheckIndex(int index, string? message = null)
        {
            if (index < 0 || index > Size)
            {
                throw new IndexOutOfRangeException(Messages.IndexOutOfBounds(this, index));
            }
        }

        public int Capacity() => Size;
        
        public IReadOnlyBuffer<T> Immutable() => BufferHelper.AllocateImmutable(Buffer);
        public IBuffer<T> Mutable() => BufferHelper.Allocate(Buffer);

        public override string ToString()
        {
            StringBuilder str = new($"{GetType().Name}={{");

            for (int i = 0; i < Size; i++)
            {
                str.Append(Buffer[i]);
                
                if (i < Size - 1)
                {
                    str.Append(", ");
                }
            }
            
            str.Append("}}");
            return str.ToString();
        }
            
    }
}