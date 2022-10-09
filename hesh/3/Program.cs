using System;
using System.Security.Cryptography;
using System.Text;



namespace hesh
{
    class Program
    {
        public static void Main()
        {
            Console.WriteLine("List of options you can do:");
            Console.WriteLine("1 - find hash via HMAC using MD5");
            Console.WriteLine("2 - find hash via HMAC using SHA1");
            Console.WriteLine("3 - find hash via HMAC using SHA256");
            Console.WriteLine("4 - find hash via HMAC using SHA384");
            Console.WriteLine("5 - find hash via HMAC using SHA512");
            Console.WriteLine("Choose what you want to do:");
            int option = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("--------------------------------------------------");
            switch (option)
            {
                case 1:
                    Console.WriteLine("Enter the massage to hesh:");
                    string str = Console.ReadLine();
                    byte[] messageArray = Encoding.Unicode.GetBytes(str);
                    var strmd5 = ComputeHashMd5(messageArray, Randomkey(messageArray));
                    Console.WriteLine($"Hash via HMACMD5: {Convert.ToBase64String(strmd5)}");
                    break;
                case 2:
                    Console.WriteLine("Enter the massage to hesh:");
                    string str1 = Console.ReadLine();
                    byte[] messageArray1 = Encoding.Unicode.GetBytes(str1);
                    var sha1 = ComputeHashSHA1(messageArray1, Randomkey(messageArray1));
                    Console.WriteLine($"Hash via HMACSHA1:{Convert.ToBase64String(sha1)}");
                    break;
                case 3:
                    Console.WriteLine("Enter the massage to hesh:");
                    string str2 = Console.ReadLine();
                    byte[] messageArray2 = Encoding.Unicode.GetBytes(str2);
                    var sha256 = ComputeHashSha256(messageArray2, Randomkey(messageArray2));
                    Console.WriteLine($"Hash via HMACSHA256:{Convert.ToBase64String(sha256)}");
                    break;
                case 4:
                    Console.WriteLine("Enter the massage to hesh:");
                    string str4 = Console.ReadLine();
                    byte[] messageArray4 = Encoding.Unicode.GetBytes(str4);
                    var sha384 = ComputeHashSHA384(messageArray4, Randomkey(messageArray4));
                    Console.WriteLine($"Hash via HMACSHA384:{Convert.ToBase64String(sha384)}");
                    break;
                case 5:
                    Console.WriteLine("Enter the massage to hesh:");
                    string str5 = Console.ReadLine();
                    byte[] messageArray5 = Encoding.Unicode.GetBytes(str5);
                    var sha512 = ComputeHashSHA512(messageArray5, Randomkey(messageArray5));
                    Console.WriteLine($"Hash via HMACSHA512:{Convert.ToBase64String(sha512)}");
                    break;
                default:
                    Console.WriteLine("Choose an option from the list above");
                    break;
            }

        }
        private static byte[] Randomkey(byte[] pas)
        {
            var rnd = new RNGCryptoServiceProvider();
            var rndNum = new byte[pas.Length];
            rnd.GetBytes(rndNum);
            for (int i = 0; i < pas.Length; i++)
            { Console.Write(rndNum[i]); }
            return rndNum;
        }
        static byte[] ComputeHashMd5(byte[] input, byte[] key)
        {
            using (var md5 = new HMACMD5(key))
            {
                return md5.ComputeHash(input);
            }
        }

        static byte[] ComputeHashSha256(byte[] input, byte[] key)
        {
            using (var sha256 = new HMACSHA256())
            {
                return sha256.ComputeHash(input);
            }
        }


        static byte[] ComputeHashSHA1(byte[] input, byte[] key)
        {
            using (var sha1 = new HMACSHA1(key))
            {
                return sha1.ComputeHash(input);
            }
        }

        static byte[] ComputeHashSHA512(byte[] input, byte[] key)
        {
            using (var sha512 = new HMACSHA512(key))
            {
                return sha512.ComputeHash(input);
            }
        }

        static byte[] ComputeHashSHA384(byte[] input, byte[] key)
        {
            using (var sha384 = new HMACSHA384(key))
            {
                return sha384.ComputeHash(input);
            }
        }

    }
}