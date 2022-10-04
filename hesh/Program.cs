using System;
using System.Security.Cryptography;
using System.Text;



namespace hesh
{
    class Program
    {
        public static void Main()
        {

            Console.WriteLine("Enter the massage to hesh:");
            string str = Console.ReadLine();
            Console.WriteLine();
            byte[] messageArray = Encoding.Unicode.GetBytes(str);
            var strmd5 = ComputeHashMd5(messageArray);
            var sha256 = ComputeHashSha256(messageArray);
            var sha1 = ComputeHashSHA1(messageArray);
            var sha384 = ComputeHashSHA384(messageArray);
            var sha512 = ComputeHashSHA512(messageArray);
            Console.WriteLine($"Hash MD5: {Convert.ToBase64String(strmd5)}");
            Guid guid1 = new Guid(strmd5);
            Console.WriteLine($"GUID: {guid1}");
            Console.WriteLine("-------------------------");
            Console.WriteLine($"Hash SHA1:{Convert.ToBase64String(sha1)}");
            Console.WriteLine("-------------------------");
            Console.WriteLine($"Hash SHA256:{Convert.ToBase64String(sha256)}");
            Console.WriteLine("-------------------------");
            Console.WriteLine($"Hash SHA384:{Convert.ToBase64String(sha384)}");
            Console.WriteLine("-------------------------");
            Console.WriteLine($"Hash SHA512:{Convert.ToBase64String(sha512)}");
            


            static byte[] ComputeHashMd5(byte[] input)
            {
                var md5 = MD5.Create();            
                return md5.ComputeHash(input);
                
            }

            static byte[] ComputeHashSha256(byte[] input)
            {
                using (var sha256 = SHA256.Create())
                {
                    return sha256.ComputeHash(input);
                }
            }

            //static byte[] ComputeHmacsha1(byte[] toBeHashed, byte[] key)
            //{
              //  using (var hmac = new HMACSHA1(key))
                //{
                  //  return hmac.ComputeHash(toBeHashed);
                //}
            //}

            static byte[] ComputeHashSHA1(byte[] input)
            {
                using (var sha1 = SHA1.Create())
                {
                    return sha1.ComputeHash(input);
                }
            }

            static byte[] ComputeHashSHA512(byte[] input)
            {
                using (var sha512 = SHA512.Create())
                {
                    return sha512.ComputeHash(input);
                }
            }

            static byte[] ComputeHashSHA384(byte[] input)
            {
                using (var sha384 = SHA384.Create())
                {
                    return sha384.ComputeHash(input);
                }
            }

        }

    }
}

