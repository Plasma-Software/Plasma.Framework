using System.Collections;

namespace Plasma.Buffer.Immutable
{
    public abstract partial class AbstractReadOnlyBuffer<T>
    {
        public IEnumerator<T> GetEnumerator() => new Enumerator(this);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        
        public class Enumerator(IReadOnlyBuffer<T> buffer, int index = 0) : IEnumerator<T>
        {
            public IReadOnlyBuffer<T> Buffer { get; } = buffer;
            public int Index { get; protected set; } = index;

            public bool MoveNext()
            {
                Index++;
                
                return true;
            }

            public void Reset()
            {
                Index = 0;
            }

            public T Current => Buffer[Index];

            object IEnumerator.Current => Current!;
            
            // idk what to dispose..
            public void Dispose() { }
        } 
    }
}