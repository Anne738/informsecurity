using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;

class Program
{
    static void Main()
    {
        Console.WriteLine("Write text to encrypt: ");
        string text = Console.ReadLine();
        RSA.AssignNewKey();
        var encrypted = RSA.EncryptData(Encoding.UTF8.GetBytes(text));
        var decrypted = RSA.DecryptData(encrypted);
        Console.WriteLine("\n\nEncrypted Text: " + Convert.ToBase64String(encrypted) +
            "\nDecrypted Text: " + Encoding.Default.GetString(decrypted)+ "\n");

    }
}
public class RSA
{
    private static RSAParameters publicKey, privateKey;
    public static void AssignNewKey()
    {
        using (var rsa = new RSACryptoServiceProvider(2048))
        {
            rsa.PersistKeyInCsp = false;
            publicKey = rsa.ExportParameters(false);
            privateKey = rsa.ExportParameters(true);
        }
    }

    public static byte[] EncryptData(byte[] dataToEncrypt)
    {
        byte[] cipherbytes;
        using (var rsa = new RSACryptoServiceProvider())
        {
            rsa.PersistKeyInCsp = false;
            rsa.ImportParameters(publicKey);
            cipherbytes = rsa.Encrypt(dataToEncrypt, true);
        }
        return cipherbytes;
    }

    public static byte[] DecryptData(byte[] dataToEncrypt)
    {
        byte[] plain;
        using (var rsa = new RSACryptoServiceProvider())
        {
            rsa.PersistKeyInCsp = false;
            rsa.ImportParameters(privateKey);
            plain = rsa.Decrypt(dataToEncrypt, true);
        }
        return plain;
    }

}

