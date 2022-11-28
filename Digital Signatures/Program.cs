﻿using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

class Program
{
    static void Main()
    {
        try
        {
            string path_plus_name = "C:/info_sec/inf_sec/DigitalSignatures/p.xml";
            Console.WriteLine("Enter the text");
            var document = Encoding.UTF8.GetBytes(Console.ReadLine());
            byte[] hashedDocument;
            using (var sha512 = SHA512.Create())
            {
                hashedDocument = sha512.ComputeHash(document);
            }
            var digitalSignature = new DigitalSignature();
            digitalSignature.GenerateNewKey(path_plus_name);
            var signature = digitalSignature.SignData(hashedDocument);
            var verified = digitalSignature.VerifySignature(path_plus_name, hashedDocument, signature);
            Console.WriteLine("Original Text = " + Encoding.Default.GetString(document) + "\n\nDigital Signature = " + Convert.ToBase64String(signature));
            Console.WriteLine(verified ? "\nThe digital signature has been correctly verified." : "The digital signature has NOT been correctly verified.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
public class DigitalSignature
{
    private readonly static string CspContainerName = "RsaContainer";
    public void GenerateNewKey(string publicKeyPath)
    {
        var cspParams = new CspParameters
        {
            KeyContainerName = CspContainerName,
            Flags = CspProviderFlags.UseMachineKeyStore
        };
        using (var rsa = new RSACryptoServiceProvider(2048, cspParams))
        {
            rsa.PersistKeyInCsp = true;
            File.WriteAllText(publicKeyPath, rsa.ToXmlString(false));
        }
    }

    public byte[] SignData(byte[] hashOfDataToSign)
    {
        var cspParams = new CspParameters
        {
            KeyContainerName = CspContainerName,
            Flags = CspProviderFlags.UseMachineKeyStore,
        };

        using (var rsa = new RSACryptoServiceProvider(2048, cspParams))
        {
            rsa.PersistKeyInCsp = false;
            var rsaFormatter = new RSAPKCS1SignatureFormatter(rsa);
            rsaFormatter.SetHashAlgorithm(nameof(SHA512));
            return rsaFormatter.CreateSignature(hashOfDataToSign);
        }
    }

    public bool VerifySignature(string publicKeyPath, byte[] hashedDocument, byte[] signature)
    {
        using (var rsa = new RSACryptoServiceProvider(2048))
        {
            rsa.PersistKeyInCsp = false;
            rsa.FromXmlString(File.ReadAllText(publicKeyPath));
            var rsaDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
            rsaDeformatter.SetHashAlgorithm(nameof(SHA512));
            return rsaDeformatter.VerifySignature(hashedDocument, signature);
        }
    }
}