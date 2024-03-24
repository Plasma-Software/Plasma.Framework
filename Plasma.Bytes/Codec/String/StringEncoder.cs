using Plasma.Bytes.Access.Writing;
using Plasma.Bytes.Codec.Encoding;

namespace Plasma.Bytes.Codec.String
{
    public class StringEncoder(int size) : IEncoder<string>
    {
        public WriteResult Encode(string value, IByteWriter writer)
        {
            int count = System.Text.Encoding.UTF8.GetByteCount(value);
            
            int max = size * 3 + 3;
            
            if (count < 1 || count > max)
            {
                throw new ArgumentException($"String exceeds maximum length of {max}"); 
            }
            
            Codec.Variable
                .VarInt()
                .Encode(count, writer);
            
            foreach (char c in value)
            {
                if (c < 128)
                {
                    writer.Write((byte) c);
                    
                    continue;
                }
                
                int character = c;
                
                List<byte> bytes = new List<byte>();
                
                while (character > 0)
                {
                    bytes.Add((byte) (character % 64 + 128));
                    character /= 64;
                }
                
                bytes[0] += 192;
                
                bytes.ForEach(b => writer.Write(b));
            }
            
            return writer.Finish();
        }
    }
}