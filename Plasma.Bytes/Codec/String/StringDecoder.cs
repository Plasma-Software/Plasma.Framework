using System.Text;
using Plasma.Bytes.Access.Reading;
using Plasma.Bytes.Codec.Decoding;

namespace Plasma.Bytes.Codec.String
{
    public class StringDecoder(int size) : IDecoder<string> {
        
        public ReadResult<string> Decode(IByteReader reader)
        {
            StringBuilder result = new StringBuilder();
            
            int count = Codec.Variable
                .VarInt()
                .Decode(reader)
                .Value;
            
            int max = size * 3 + 3;
            
            if (count < 1 || count > max)
            {
                throw new ArgumentException($"String exceeds maximum length of {max}");
            }
            
            int i = 0;
            
            while (true)
            {
                if (i == count) break;
                
                byte current = reader.Read();
                
                if (current < 128)
                {
                    result.Append((char) current);
                    i++;
                    continue;
                }
               
                int bytes = 1;
                byte temp = current;
                
                while ((temp & (1 << (7 - bytes))) != 0)
                {
                    bytes++;
                }
                
                int character = temp & ((1 << (8 - bytes)) - 1);
                i++;
                
                for (int j = i; j < bytes - 1; j++)
                {
                    character = (character << 6) | (current & 63);
                    i++;
                }
                
                result.Append((char) character);
            }
            
            return reader.Finish(result.ToString());
        }
    }
}