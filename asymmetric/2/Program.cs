using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;



class Program
{
    static void Main()
    {
        string original;
        byte[] text;
        string xtmf = "..\\..\\..\\.\\f\\", encf = "..\\..\\..\\.\\ef\\";
        string pathfilextm, pathfiletnc;
        string txtFileName;

        do
        {
            Console.Write("1 - generate new keys\n2 - encypt\n3 - decrypt\n4 - delete keys\n5 - exit\n");
            switch (Convert.ToInt32(Console.ReadLine()))
            {
                case 1:
                    pathfilextm = xtmf + "Anna_Bulgakova" + ".xml";
                    RSA.GenerateOwnKeys(pathfilextm);
                    Console.WriteLine("\ndone\n\n");
                    break;

                case 2:
                    try
                    {
                        var files = Directory.GetFiles(xtmf);
                        if (files.Length != 0)
                        {
                            Console.Write("Enter data to encrypt: ");
                            original = Console.ReadLine();

                            Console.WriteLine("Public keys:");
                            for (int i = 0; i < files.Length; i++)
                            {
                                Console.WriteLine((i + 1) + "  " + files[i]);
                            }

                            Console.Write("\nPublic key to encrypt (enter number): ");
                            int num = Convert.ToInt32(Console.ReadLine());
                            pathfilextm = files[num - 1];

                            Console.Write("Name file /not extantion/: ");
                            txtFileName = Console.ReadLine();
                            pathfiletnc = encf + txtFileName + ".txt";

                            RSA.EncryptData(pathfilextm, Encoding.UTF8.GetBytes(original), pathfiletnc);

                            Console.WriteLine("\ndone\n\n");
                        }
                        else
                        {
                            Console.WriteLine("NO PUBLIC KEYS");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                    break;

                case 3:
                    try
                    {
                        var files = Directory.GetFiles(encf);
                        if (files.Length != 0)
                        {
                            Console.WriteLine("Encrypted messages:");
                            for (int i = 0; i < files.Length; i++)
                            {
                                Console.WriteLine((i + 1) + ". " + files[i]);
                            }

                            Console.Write("\nChoose message to dencrypt (enter number): ");
                            int num = Convert.ToInt32(Console.ReadLine());
                            pathfiletnc = files[num - 1];

                            text = RSA.DecryptData(pathfiletnc);

                            Console.WriteLine("Original text: " + Encoding.Default.GetString(text));
                        }
                        else
                        {
                            Console.WriteLine("NO FILES TO DECRYPT");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }

                    break;
                case 4:
                    try
                    {
                        var files = Directory.GetFiles(xtmf);
                        for (int i = 0; i < files.Length; i++)
                        {
                            File.Delete(files[i]);
                        }

                        Console.WriteLine("\ndone\n\n");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                    break;
                case 5:
                    return;
                default:
                    break;
            }
        } while (true);
    }
}
public class RSA
{
    const string CspContainerName = "MyContainer";
    public static void GenerateOwnKeys(string publicKeyPath)
    {
        CspParameters cspParameters = new CspParameters(1)
        {
            KeyContainerName = CspContainerName,
            Flags = CspProviderFlags.UseMachineKeyStore,
            ProviderName = "Microsoft Strong Cryptographic Provider",
        };
        using (var rsa = new RSACryptoServiceProvider(2048, cspParameters))
        {
            rsa.PersistKeyInCsp = true;
            File.WriteAllText(publicKeyPath, rsa.ToXmlString(false));
        }
    }
    public static void DeleteKeys(string publicKeyPath)
    {
        CspParameters cspParameters = new CspParameters
        {
            KeyContainerName = CspContainerName,
            Flags = CspProviderFlags.UseMachineKeyStore
        };
        var rsa = new RSACryptoServiceProvider(cspParameters)
        {
            PersistKeyInCsp = false
        };
        File.Delete(publicKeyPath);
        rsa.Clear();
    }
    public static void EncryptData(string publicKeyPath, byte[] dataToEncrypt, string chipherTextPath)
    {
        byte[] chipherBytes;
        using (var rsa = new RSACryptoServiceProvider(2048))
        {
            rsa.PersistKeyInCsp = false;
            rsa.FromXmlString(File.ReadAllText(publicKeyPath));
            chipherBytes = rsa.Encrypt(dataToEncrypt, true);
        }
        File.WriteAllBytes(chipherTextPath, chipherBytes);
    }
    public static byte[] DecryptData(string chipherTextPath)
    {
        byte[] chipherBytes = File.ReadAllBytes(chipherTextPath);
        byte[] plainTextBytes;
        var cspParams = new CspParameters
        {
            KeyContainerName = CspContainerName,
            Flags = CspProviderFlags.UseMachineKeyStore
        };
        using (var rsa = new RSACryptoServiceProvider(2048, cspParams))
        {
            rsa.PersistKeyInCsp = true;
            plainTextBytes = rsa.Decrypt(chipherBytes, true);
        }
        return plainTextBytes;
    }
}
