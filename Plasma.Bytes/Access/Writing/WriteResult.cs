namespace Plasma.Bytes.Access.Writing
{
    public readonly struct WriteResult(int index)
    {
        public int Index { get; } = index;
    }
}