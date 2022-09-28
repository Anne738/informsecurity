using System.Security.Cryptography;
using System;
using System.IO;
using System.Text;


namespace xor
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] readContent = File.ReadAllBytes(@"C:\KTC Net 2\mit.txt").ToArray();
            Console.WriteLine(Encoding.UTF8.GetString(readContent));
            Console.WriteLine("1 - encrypt file");
            Console.WriteLine("2 - decrypt file");
            Console.Write("Choose ");

            string option = Console.ReadLine();
            string path = @"C:\KTC Net 2\mit.txt";

            if (option == "1")
            {
                Console.WriteLine("Write password");
                string keyword = Console.ReadLine();
                Console.WriteLine("-------------------------------");
                Console.Write(keyword);
                byte[] secret = Encoding.UTF8.GetBytes(keyword);


                byte[] data = File.ReadAllBytes(path).ToArray();

                int length = data.Length;

                byte[] encr = new byte[length];

                for (int i = 0; i < length; i++)
                {
                    encr[i] = (byte)(data[i] ^ secret[i % secret.Length]); 
                }
                File.WriteAllBytes(path + "file.dat", encr);
            }
            else if (option == "2")
            {
                Console.WriteLine("Write password");
                string keyword = Console.ReadLine();
                Console.WriteLine("-------------------------------");
                Console.Write(keyword);
                byte[] secret = Encoding.UTF8.GetBytes(keyword);

                byte[] data = File.ReadAllBytes(path + "file.dat").ToArray();

                int length = data.Length;

                byte[] encr = new byte[length];
                for (int i = 0; i < length; i++)
                {
                    encr[i] = (byte)(data[i] ^ secret[i % secret.Length]);
                }
                File.WriteAllBytes(path + "file.txt", encr);

            }
        }
    }
}