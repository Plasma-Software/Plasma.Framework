using Plasma.Buffer.Immutable;
using Plasma.Buffer.Mutable;

namespace Plasma.Buffer
{
    public static class BufferHelper
    {

        
        // Alloc //
        
        // Immutable //
        public static IReadOnlyBuffer<T> AllocateImmutable<T>(int capacity) => new ReadOnlyBuffer<T>(capacity);
        public static IReadOnlyBuffer<T> AllocateImmutable<T>(T[] buffer) => new ReadOnlyBuffer<T>(buffer);
        public static IReadOnlyBuffer<T> AllocateImmutable<T>(IEnumerable<T> buffer) => new ReadOnlyBuffer<T>(buffer);
        // Immutable //
        
        // Mutable //
        public static IBuffer<T> Allocate<T>(int capacity) => new Buffer<T>(capacity);
        public static IBuffer<T> Allocate<T>(T[] buffer) => new Buffer<T>(buffer);
        public static IBuffer<T> Allocate<T>(IEnumerable<T> buffer) => new Buffer<T>(buffer);
        
        // Alloc //
        
        public static IReadOnlyBuffer<T> Concat<T>(IReadOnlyBuffer<T> buffer, params IReadOnlyBuffer<T>[] buffers)
        {
            int size = buffer.Size;

            foreach (IReadOnlyBuffer<T> buf in buffers)
            {
                size += buf.Size;
            }
         
            IBuffer<T> alloc = Allocate<T>(size);
            
            int copyIdx = 0;
            
            buffer.CopyTo(ref alloc, copyIdx);
            
            copyIdx += buffer.Size;
            
            foreach (IReadOnlyBuffer<T> buf in buffers)
            {
                buf.CopyTo(ref alloc, copyIdx);
                
                copyIdx += buf.Size;
            }
            
            return alloc.Immutable();
        }
        
        public static IReadOnlyBuffer<T> Merge<T>(IReadOnlyBuffer<T> buffer, params IReadOnlyBuffer<T>[] buffers)
        {
            int size = buffer.Size;

            foreach (IReadOnlyBuffer<T> buf in buffers)
            {
                if (buf.Size > size)
                {
                    size = buf.Size;
                }
            }
            
            IBuffer<T> alloc = Allocate<T>(size);
            
            buffer.CopyTo(ref alloc);
            
            foreach (IReadOnlyBuffer<T> buf in buffers)
            {
                InnerMerge(ref alloc, buf);
            }
            
            return alloc.Immutable();
        }
        
        public static IReadOnlyBuffer<T> InnerMerge<T>(ref IBuffer<T> alloc, IReadOnlyBuffer<T> buffer)
        {
            for (int i = 0; i < buffer.Size; i++)
            {
                if (EqualityComparer<T>.Default.Equals(alloc[i], alloc.Empty))
                {
                    alloc[i] = buffer[i];
                }
            }
            
            return alloc;
        }
    }
}