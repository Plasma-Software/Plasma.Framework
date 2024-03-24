namespace Plasma.Bytes.Codec
{
    public class NumericHelper
    {
        public static byte[] Encode(Int128 value, int length = 16)
        {
            if (length <= 1 || length >= 128)
            {
                throw new ArgumentException("Length must be between 1 and 128 bits.", nameof(length));
            }
            
            Int128 max = (1 << (length - 1)) - 1;
            
            if (value < -max || value > max)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Value is out of range for the specified length.");
            }
            
            int bytes = (length + 7) / 8;
            
            byte[] result = new byte[bytes];
            
            if (value >= 0)
            {
                for (int i = 0; i < bytes; i++)
                {
                    int shift = (bytes - i - 1) * 8;
                    result[i] = (byte) ((value >> shift) & 0xFF);
                }
            }
            else
            {
                Int128 abs = -value;
                
                for (int i = 0; i < bytes; i++)
                {
                    int shift = (bytes - i - 1) * 8;
                    result[i] = (byte) (((abs >> shift) ^ 0xFF) + 1);
                }
            }
            
            return result;
        }
        
        public static byte[] Encode(UInt128 value, int length = 16)
        {
            if (length <= 0 || value >= 128)
            {
                throw new ArgumentException("Length must be between 1 and 128 bits.", nameof(length));
            }

            UInt128 max = (UInt128) (1 << (length - 1)) - 1;
            
            if (value > max)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Value is out of range for the specified length.");
            }
            
            int bytes = (length + 7) / 8;
            
            byte[] result = new byte[bytes];

            for (int i = 0; i < bytes; i++)
            {
                int shift = (bytes - i - 1) * 8;
                result[i] = (byte) ((value >> shift) & 0xFF);
            }

            return result;
        }
        
        public static Int128 Decode(byte[] bytes, int length = 16)
        {
            if (bytes == null || bytes.Length == 0)
            {
                throw new ArgumentException("Bytes array cannot be null or empty.", nameof(bytes));
            }
       
            if (bytes.Length <= 0 || bytes.Length >= 128)
            {
                throw new ArgumentException("Length must be between 1 and 128 bits.", nameof(bytes.Length));
            }
            
            int bytesLength = (length + 7) / 8;

            if (bytes.Length != bytesLength)
            {
                throw new ArgumentException($"Length of byte array must be {bytesLength} bytes for the specified length.", nameof(bytes));
            }

            Int128 result = 0;

            for (int i = 0; i < bytesLength; i++)
            {
                result <<= 8;
                result |= bytes[i];
            }

            Int128 max = (1 << (length - 1)) - 1;
            
            if ((bytes[0] & 0x80) != 0 && result > max)
            {
                result -= 1 << length;
            }
            
            return result;
        }
    }
}