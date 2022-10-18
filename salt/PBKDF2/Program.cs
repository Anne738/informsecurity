using System;
using System.Security.Cryptography;
using System.Text;
using System.Diagnostics;



//4.Розробити клас PBKDF2, що має наступну функціональність: генерує "сіль", задає алгоритм хешування (MD5, SHA1, SHA256, SHA384, SHA512) 
//та обчислює хеш для заданого числа ітерацій. Створити програму, що обчислює час, витрачений на обчислення хешу для різного числа ітерацій 
//(10 значень із кроком 50'000; перше значення = номер варіанта * 10'000 ). Побудувати графік залежності витраченого часу від числа ітерацій.


class PBKDF2
{
    static void Main()
    {
        
        Console.WriteLine("List of options you can do:\n1 - hash via HMAC using MD5\n2 - hash via HMAC using SHA1\n3 - hash via HMAC using SHA256\n4 - hash via HMAC using SHA384\n5 - hash via HMAC using SHA512\nChoose what you want to do:");
        int option = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("--------------------------------------------------");
        
        switch (option)
        {
            case 1:
                Console.WriteLine("Enter the massage to hesh: ");
                string str = Console.ReadLine();
                byte[] messageArray = Encoding.Unicode.GetBytes(str);
                var strmd5 = Convert.ToBase64String(ComputeHashMd5(messageArray));
                Console.WriteLine("Password to hash : " + strmd5);
                Hh(strmd5);
                break;
            case 2:
                Console.WriteLine("Enter the massage to hesh: ");
                string str2 = Console.ReadLine();
                byte[] messageArray2 = Encoding.Unicode.GetBytes(str2);
                var strsha1 = Convert.ToBase64String(ComputeHashSHA1(messageArray2));
                Console.WriteLine("Password to hash : " + strsha1);
                Hh(strsha1);
                break;
            case 3:
                Console.WriteLine("Enter the massage to hesh: ");
                string str3 = Console.ReadLine();
                byte[] messageArray3 = Encoding.Unicode.GetBytes(str3);
                var strsha256 = Convert.ToBase64String(ComputeHashSha256(messageArray3));
                Console.WriteLine("Password to hash : " + strsha256);
                Hh(strsha256);
                break;
            case 4:
                Console.WriteLine("Enter the massage to hesh: ");
                string str4 = Console.ReadLine();
                byte[] messageArray4 = Encoding.Unicode.GetBytes(str4);
                var strsha384 = Convert.ToBase64String(ComputeHashSHA384(messageArray4));
                Console.WriteLine("Password to hash : " + strsha384);
                Hh(strsha384);
                break;
            case 5:
                Console.WriteLine("Enter the massage to hesh: ");
                string str5 = Console.ReadLine();
                byte[] messageArray5 = Encoding.Unicode.GetBytes(str5);
                var strsha512 = Convert.ToBase64String(ComputeHashSHA512(messageArray5));
                Console.WriteLine("Password to hash : " + strsha512);
                Hh(strsha512);
                break;



        }
    }


private static void HashPassword(string passwordToHash, int numberOfRounds)
    {
        var sw = new Stopwatch();
        sw.Start();
        var hashedPassword = PBKDF2.HashPassword(Encoding.UTF8.GetBytes(passwordToHash),PBKDF2.GenerateSalt(),numberOfRounds);
        sw.Stop();
        Console.WriteLine("Hashed Password : " + Convert.ToBase64String(hashedPassword));
        Console.WriteLine("Iterations <" + numberOfRounds + "> Time: " + sw.ElapsedMilliseconds + "ms");
    }

public static void Hh(string passwordToHash)
    {
        //const string passwordToHash = "VeryComplexPassword";
        int iter = 50000;
        for (int i = 0; i < 10; i++)
        {
            HashPassword(passwordToHash, iter);
            iter+=50000;
        }

        Console.ReadLine();
    }
        




    public static byte[] GenerateSalt()
    {
        using (var randomNumberGenerator = new RNGCryptoServiceProvider())
        {
            var randomNumber = new byte[32];
            randomNumberGenerator.GetBytes(randomNumber);
            return randomNumber;
        }
    }
    public static byte[] HashPassword(byte[] toBeHashed, byte[] salt, int numberOfRounds)
    {
        using (var rfc2898 = new Rfc2898DeriveBytes(toBeHashed, salt, numberOfRounds))
        {
            return rfc2898.GetBytes(20);
        }
    }



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



//var stopwatch = new Stopwatch();

//stopwatch.Start();
// processing 
//stopwatch.Stop();
//Console.WriteLine($"Iterations number = {iterationsNumber}; Spent time = {stopwatch.ElapsedMilliseconds}");
//stopwatch.Reset();