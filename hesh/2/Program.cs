using System;
using System.Security.Cryptography;
using System.Text;

//відповідь 20192020, через те що while робить на одну ініціалізацію більше

namespace hesh
{
    class Program
    {
        public static void Main()
        {
            String correct = "po1MVkAE7IjUUwu61XxgNg=="; 
            String attempt = "";
            int c = 100000000;
            while (attempt!=correct)
            {

                string str = c.ToString();
                str = str[1..];
                byte[] messageArray = Encoding.Unicode.GetBytes(str);
                messageArray = ComputeHashMd5(messageArray);
                attempt = Convert.ToBase64String(messageArray);
                c++;
                Console.WriteLine(str + "         " + attempt);
            }
            c--;
            string c_str = c.ToString();
            c_str = c_str[1..];
            Console.WriteLine(c_str + " - password");

            static byte[] ComputeHashMd5(byte[] input)
            {
                var md5 = MD5.Create();
                return md5.ComputeHash(input);
            }
        }
    }
}