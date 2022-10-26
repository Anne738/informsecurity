using System.Text;



class XOR
{
    public static void Main(string[] args)
    {
        byte[] readContent = File.ReadAllBytes(@"C:\KTC Net 2\mit.txt").ToArray();
        Console.WriteLine(Encoding.UTF8.GetString(readContent));
        Console.WriteLine("1 - encrypt file");
        Console.WriteLine("2 - decrypt file");
        Console.Write("Choose ");
        int option = Convert.ToInt32(Console.ReadLine());
        string path = @"C:\KTC Net 2\mit.txt";

        switch (option)
        {
            case 1:
                {
                    var exor = new XOR_Program();
                    Console.WriteLine("Write password");
                    string keyword = Console.ReadLine();
                    byte[] password = Encoding.UTF8.GetBytes(keyword);
                    var encr = exor.XoR(readContent, password);
                    Console.WriteLine("Done");
                    File.WriteAllBytes(path + "file.dat", encr);
                    break;
                }
            case 2:
                {
                    byte[] encryptContent = File.ReadAllBytes(path + "file.dat").ToArray();
                    var dxor = new XOR_Program();
                    Console.WriteLine("Write password");
                    string keyword = Console.ReadLine();
                    byte[] password = Encoding.UTF8.GetBytes(keyword);
                    var decr = dxor.XoR(encryptContent, password);
                    Console.WriteLine("Done");
                    File.WriteAllBytes(path + "file.txt", decr);
                    break;
                }
        }
    }

    public class XOR_Program
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
        public byte[] XoR(byte[] text, byte[] key)
        {
            for (int i = 0; i < text.Length; i++)
            {
                text[i] = (byte)(text[i] ^ GetKey(key, text)[i]);
            }
            return text;
        }
    }
}
