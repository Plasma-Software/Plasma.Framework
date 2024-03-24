namespace Plasma.Buffer
{
    public static class BufferExtensions
    {
        // ReadOnlyBuffer //
        public static T[] Array<T>(this IReadOnlyBuffer<T> buffer)
        {
            T[] array = new T[buffer.Size];
            
            for (int i = 0; i < buffer.Size; i++)
            {
                array[i] = buffer[i];
            }
            
            return array;
        }
        
        public static ICollection<T> Collection<T>(this IReadOnlyBuffer<T> buffer)
        {
            ICollection<T> collection = new List<T>();
            
            for (int i = 0; i < buffer.Size; i++)
            {
                collection.Add(buffer[i]);
            }
            
            return collection;
        }
        
        public static List<T> List<T>(this IReadOnlyBuffer<T> buffer) => (List<T>) Collection(buffer);
        // ReadOnlyBuffer //
    }
}