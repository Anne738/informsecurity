using System.Text;

class Pass
{
    static void Main()
    {
        byte[] file_enc = File.ReadAllBytes(@"C:\KTC Net 2\encfile5.dat").ToArray();
        string result = @"C:\KTC Net 2\result.txt";
        string fraza = "Mit21";
        byte[] b = Encoding.UTF8.GetBytes(fraza);
        byte[] key = new byte[5];
        var x = new XOR();

        for (int i = 0; i < file_enc.Length - fraza.Length; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                key[j] = file_enc[j + i];
            }
            var pass = x.Xor(key, b);
            var text = x.Xor(file_enc, pass);
            string dec = Encoding.UTF8.GetString(text);
            if (dec.Contains(" Mit21 "))
            {
                File.AppendAllText(result, Encoding.UTF8.GetString(pass) + "\n" + dec);
                Console.WriteLine("Done");
            }
        }
    }

    public class XOR
    {
        private byte[] GetKey(byte[] key, byte[] array)
        {
            byte[] secret = new byte[array.Length];
            for (int i = 0; i < secret.Length; i++)
            {
                secret[i] = key[i % key.Length];
            }
            return secret;
        }
        public byte[] Xor(byte[] text, byte[] key)
        {
            for (int i = 0; i < text.Length; i++)
            {
                text[i] = (byte)(text[i] ^ GetKey(key, text)[i]);
            }
            return text;
        }
    }
}