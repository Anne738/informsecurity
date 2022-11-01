using System.Security.Cryptography;
using System.Text;


class Program
{
    static void Main()
    {
        do
        {
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("Choose: \n1 - DES \n2 - Triple DES \n3 - AES \n4 - exit");
            Console.WriteLine("--------------------------------------------------");
            int option = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\n\nEnter text:");
            string input = Console.ReadLine();
            Console.WriteLine("Enter the password for your text:");
            byte[] hash = PBKDF2.HashPassword(Console.ReadLine(), 50000, GenerateRandomNumber(32));
            SymmetricAlgorithm algo;
            var k = new Algos();
            switch (option)
            {
                case 1:
                    var key = Key(8, hash);
                    var iv = Iv(8, hash);
                    algo = DESCryptoServiceProvider.Create();
                    Console.WriteLine("\n\nOriginal text: " + input);
                    var encrypted_des = k.Encryption(Encoding.UTF8.GetBytes(input), key, iv, algo);
                    Console.WriteLine("Encrypted text: " + Convert.ToBase64String(encrypted_des));
                    var decrypted_des = k.Decryption(encrypted_des, key, iv, algo);
                    Console.WriteLine("Decrypted text: " + Encoding.UTF8.GetString(decrypted_des) + "\n");
                    break;
                case 2:
                    var key2 = Key(16, hash);
                    var iv2 = Iv(8, hash);
                    algo = TripleDESCryptoServiceProvider.Create();
                    Console.WriteLine("Original text: " + input);
                    var encrypted_td = k.Encryption(Encoding.UTF8.GetBytes(input), key2, iv2, algo);
                    Console.WriteLine("Encrypted text: " + Convert.ToBase64String(encrypted_td));
                    var decrypted = k.Decryption(encrypted_td, key2, iv2, algo);
                    Console.WriteLine("Decrypted text: " + Encoding.UTF8.GetString(decrypted) + "\n");
                    break;
                case 3:
                    var key3 = Key(32, hash);
                    var iv3 = Iv(16, hash);
                    algo = AesCryptoServiceProvider.Create();
                    Console.WriteLine("Original text: " + input);
                    var encrypted_aes = k.Encryption(Encoding.UTF8.GetBytes(input), key3, iv3, algo);
                    Console.WriteLine("Encrypted text: " + Convert.ToBase64String(encrypted_aes));
                    var decrypted_aes = k.Decryption(encrypted_aes, key3, iv3, algo);
                    Console.WriteLine("Decrypted text: " + Encoding.UTF8.GetString(decrypted_aes) + "\n");
                    break;
                case 4:
                    return;
                default:
                    break;
            }
        }
        while (true);
    }

    
    public static byte[] GenerateRandomNumber(int length)
    {
        using (var randomNumberGenerator = new RNGCryptoServiceProvider())
        {
            var randomNumber = new byte[length];
            randomNumberGenerator.GetBytes(randomNumber);
            return randomNumber;
        }
    }

    public static byte[] Key(int length, byte[] hash)
    {
        var KEY = new byte[length];
        int key = 0;
        for (int i = 0; i < length; i++)
        {
            KEY[key] = hash[i];
            key++;
        }
        return KEY;
    }

    public static byte[] Iv(int length, byte[] hash)
    {
        var IV = new byte[length];
        int iv = 0;
        for (int i = hash.Length - 1; i != hash.Length - length; i--)
        {
            IV[iv] = hash[i];
            iv++;
        }
        return IV;
    }
}


public class Algos
{
    public byte[] Encryption(byte[] toEncrypt, byte[] key, byte[] iv, SymmetricAlgorithm symmetricAlgorithm)
    {
        using (var ag = symmetricAlgorithm)
        {
            ag.Mode = CipherMode.CBC;
            ag.Padding = PaddingMode.PKCS7;
            ag.Key = key;
            ag.IV = iv;
            using (var memoryStream = new MemoryStream())
            {
                var cryptoStream = new CryptoStream(memoryStream, ag.CreateEncryptor(), CryptoStreamMode.Write);
                cryptoStream.Write(toEncrypt, 0, toEncrypt.Length);
                cryptoStream.FlushFinalBlock();
                return memoryStream.ToArray();
            }
        }
    }

    public byte[] Decryption(byte[] toDecrypt, byte[] key, byte[] iv, SymmetricAlgorithm symmetricAlgorithm)
    {
        using (var ag = symmetricAlgorithm)
        {
            ag.Mode = CipherMode.CBC;
            ag.Padding = PaddingMode.PKCS7;
            ag.Key = key;
            ag.IV = iv;
            using (var memoryStream = new MemoryStream())
            {
                var cryptoStream = new CryptoStream(memoryStream, ag.CreateDecryptor(), CryptoStreamMode.Write);
                cryptoStream.Write(toDecrypt, 0, toDecrypt.Length);
                cryptoStream.FlushFinalBlock();
                return memoryStream.ToArray();
            }
        }
    }
}

public class PBKDF2
{
    public static byte[] HashPassword(string passwordToHash, int numOfRounds, byte[] generated_salt)
    {
        var hashedPassword = HashPasswordhash(Encoding.UTF8.GetBytes(passwordToHash), generated_salt, numOfRounds);
        return hashedPassword;
    }

    public static byte[] HashPasswordhash(byte[] toBeHashed, byte[] generated_salt, int numOfRounds)
    {
        using (var rfc2898 = new Rfc2898DeriveBytes(toBeHashed, generated_salt, numOfRounds, HashAlgorithmName.SHA256))
        {
            return rfc2898.GetBytes(32);
        }
    }
}