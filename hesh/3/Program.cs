using System;
using System.Security.Cryptography;
using System.Text;



namespace hesh
{
    class Program
    {
        public static void Main()
        {
            Console.WriteLine("List of options you can do:\n1 - hash via HMAC using MD5\n2 - hash via HMAC using SHA1\n3 - hash via HMAC using SHA256\n4 - hash via HMAC using SHA384\n5 - hash via HMAC using SHA512\nChoose what you want to do:");
            int option = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("--------------------------------------------------");
            switch (option)
            {
                case 1:
                    Console.WriteLine("List of options you can do:\n1 - find hash via HMAC\n2 - verification of authenticity");
                    int option1 = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("--------------------------------------------------");
                    switch (option1)
                    {
                        case 1:
                            Console.WriteLine("Enter the massage to hesh: ");
                            string str = Console.ReadLine();
                            byte[] messageArray = Encoding.Unicode.GetBytes(str);
                            Console.WriteLine("Enter key: ");
                            string key = Console.ReadLine();
                            byte[] key_byte = Encoding.Unicode.GetBytes(key);
                            //Console.WriteLine(Convert.ToBase64String(Randomkey(messageArray)));
                            var strmd5 = ComputeHashMd5(messageArray, key_byte);
                            Console.WriteLine($"\n Hash via HMACMD5: {Convert.ToBase64String(strmd5)}");
                            break;
                        case 2:
                            Console.WriteLine("Enter the massage: ");
                            string str_1 = Console.ReadLine();
                            byte[] messageArray_1 = Encoding.Unicode.GetBytes(str_1);
                            Console.WriteLine("Enter key: ");
                            string key_1 = Console.ReadLine();                           
                            byte[] key_byte_1 = Encoding.Unicode.GetBytes(key_1);
                            var strmd5_1 = ComputeHashMd5(messageArray_1, key_byte_1);
                            string heshnew_1 = Convert.ToBase64String(strmd5_1);
                            Console.WriteLine("Enter hesh: ");
                            string hesh = Console.ReadLine();
                            if (hesh == heshnew_1)
                                Console.WriteLine("Your message is not corrupted");
                            else
                                Console.WriteLine("Error");
                            break;
                    }
                    
                    break;
                case 2:
                    Console.WriteLine("List of options you can do:\n1 - find hash via HMAC\n2 - verification of authenticity");
                    int option2 = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("--------------------------------------------------");
                    switch (option2)
                    {
                        case 1:
                            Console.WriteLine("Enter the massage to hesh:");
                            string str1 = Console.ReadLine();
                            byte[] messageArray1 = Encoding.Unicode.GetBytes(str1);
                            Console.WriteLine("Enter key: ");
                            string key = Console.ReadLine();
                            byte[] key_byte = Encoding.Unicode.GetBytes(key);
                            var sha1 = ComputeHashSHA1(messageArray1, key_byte);
                            Console.WriteLine($"\n Hash via HMACSHA1:{Convert.ToBase64String(sha1)}");
                            break;
                        case 2:
                            Console.WriteLine("Enter the massage: ");
                            string str_1 = Console.ReadLine();
                            byte[] messageArray_1 = Encoding.Unicode.GetBytes(str_1);
                            Console.WriteLine("Enter key: ");
                            string key_1 = Console.ReadLine();
                            byte[] key_byte_1 = Encoding.Unicode.GetBytes(key_1);
                            var strmd5_1 = ComputeHashSHA1(messageArray_1, key_byte_1);
                            string heshnew_1 = Convert.ToBase64String(strmd5_1);
                            Console.WriteLine("Enter hesh: ");
                            string hesh = Console.ReadLine();
                            if (hesh == heshnew_1)
                                Console.WriteLine("Your message is not corrupted");
                            else
                                Console.WriteLine("Error");
                            break;
                    }
                    
                    break;
                case 3:
                    Console.WriteLine("List of options you can do:\n1 - find hash via HMAC\n2 - verification of authenticity");
                    int option3 = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("--------------------------------------------------");
                    switch (option3)
                    {
                        case 1:
                            Console.WriteLine("Enter the massage to hesh:");
                            string str2 = Console.ReadLine();
                            byte[] messageArray2 = Encoding.Unicode.GetBytes(str2);
                            Console.WriteLine("Enter key: ");
                            string key = Console.ReadLine();
                            byte[] key_byte = Encoding.Unicode.GetBytes(key);
                            var sha256 = ComputeHashSha256(messageArray2, (messageArray2));
                            Console.WriteLine($"\n Hash via HMACSHA256:{Convert.ToBase64String(sha256)}");
                            break;
             
                        case 2:
                            Console.WriteLine("Enter the massage: ");
                            string str_1 = Console.ReadLine();
                            byte[] messageArray_1 = Encoding.Unicode.GetBytes(str_1);
                            Console.WriteLine("Enter key: ");
                            string key_1 = Console.ReadLine();
                            byte[] key_byte_1 = Encoding.Unicode.GetBytes(key_1);
                            var strmd5_1 = ComputeHashSha256(messageArray_1, key_byte_1);
                            string heshnew_1 = Convert.ToBase64String(strmd5_1);
                            Console.WriteLine("Enter hesh: ");
                            string hesh = Console.ReadLine();
                            if (hesh == heshnew_1)
                                Console.WriteLine("Your message is not corrupted");
                            else
                                Console.WriteLine("Error");
                            break;
                    }

                    break;
                    
                case 4:
                    Console.WriteLine("List of options you can do:\n1 - find hash via HMAC\n2 - verification of authenticity");
                    int option4 = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("--------------------------------------------------");
                    switch (option4)
                    {
                        case 1:
                            Console.WriteLine("Enter the massage to hesh:");
                            string str4 = Console.ReadLine();
                            byte[] messageArray4 = Encoding.Unicode.GetBytes(str4);
                            Console.WriteLine("Enter key: ");
                            string key = Console.ReadLine();
                            byte[] key_byte = Encoding.Unicode.GetBytes(key);
                            var sha384 = ComputeHashSHA384(messageArray4, (messageArray4));
                            Console.WriteLine($"\n Hash via HMACSHA384:{Convert.ToBase64String(sha384)}");
                            break;

                        case 2:
                            Console.WriteLine("Enter the massage: ");
                            string str_1 = Console.ReadLine();
                            byte[] messageArray_1 = Encoding.Unicode.GetBytes(str_1);
                            Console.WriteLine("Enter key: ");
                            string key_1 = Console.ReadLine();
                            byte[] key_byte_1 = Encoding.Unicode.GetBytes(key_1);
                            var strmd5_1 = ComputeHashSHA384(messageArray_1, key_byte_1);
                            string heshnew_1 = Convert.ToBase64String(strmd5_1);
                            Console.WriteLine("Enter hesh: ");
                            string hesh = Console.ReadLine();
                            if (hesh == heshnew_1)
                                Console.WriteLine("Your message is not corrupted");
                            else
                                Console.WriteLine("Error");
                            break;
                    }

                    break;
                    
                case 5:
                    Console.WriteLine("List of options you can do:\n1 - find hash via HMAC\n2 - verification of authenticity");
                    int option5 = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("--------------------------------------------------");
                    switch (option5)
                    {
                        case 1:
                            Console.WriteLine("Enter the massage to hesh:");
                            string str5 = Console.ReadLine();
                            byte[] messageArray5 = Encoding.Unicode.GetBytes(str5);
                            Console.WriteLine("Enter key: ");
                            string key = Console.ReadLine();
                            byte[] key_byte = Encoding.Unicode.GetBytes(key);
                            var sha512 = ComputeHashSHA512(messageArray5, (messageArray5));
                            Console.WriteLine($"\n Hash via HMACSHA512:{Convert.ToBase64String(sha512)}");
                            break;

                        case 2:
                            Console.WriteLine("Enter the massage: ");
                            string str_1 = Console.ReadLine();
                            byte[] messageArray_1 = Encoding.Unicode.GetBytes(str_1);
                            Console.WriteLine("Enter key: ");
                            string key_1 = Console.ReadLine();
                            byte[] key_byte_1 = Encoding.Unicode.GetBytes(key_1);
                            var strmd5_1 = ComputeHashSHA512(messageArray_1, key_byte_1);
                            string heshnew_1 = Convert.ToBase64String(strmd5_1);
                            Console.WriteLine("Enter hesh: ");
                            string hesh = Console.ReadLine();
                            if (hesh == heshnew_1)
                                Console.WriteLine("Your message is not corrupted");
                            else
                                Console.WriteLine("Error");
                            break;
                    }

                    break;
                    
                default:
                    Console.WriteLine("Choose an option from the list above");
                    break;
            }

        }
        /*private static byte[] Randomkey(byte[] pas)
        {
            var rnd = new RNGCryptoServiceProvider();
            var rndNum = new byte[pas.Length];
            rnd.GetBytes(rndNum);
            for (int i = 0; i < pas.Length; i++)
            { }//{ Console.Write(rndNum[i]); }
            return rndNum;
        }*/

        static byte[] ComputeHashMd5(byte[] input, byte[] key)
        {
            using (var md5 = new HMACMD5(key))
            {
                return md5.ComputeHash(input);
            }
        }

        static byte[] ComputeHashSha256(byte[] input, byte[] key)
        {
            using (var sha256 = new HMACSHA256(key))
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