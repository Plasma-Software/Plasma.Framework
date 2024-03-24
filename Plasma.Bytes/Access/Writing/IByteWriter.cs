using Plasma.Bytes.Buffer;
using Plasma.Bytes.Wrapper;

namespace Plasma.Bytes.Access.Writing
{
    public interface IByteWriter
    {
        public IByteBuffer Buffer { get; }
        public int Index { get; }
        
        IByteWriter Write(byte value);
        IByteWriter WriteMany(params byte[] values);
        
        IByteWriter Reset();
        
        WriteResult Finish();
    }
}