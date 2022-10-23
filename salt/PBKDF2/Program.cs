using System;
using System.Security.Cryptography;
using System.Text;
using System.Diagnostics;



//4.Розробити клас PBKDF2, що має наступну функціональність: генерує "сіль", задає алгоритм хешування (MD5, SHA1, SHA256, SHA384, SHA512) 
//та обчислює хеш для заданого числа ітерацій. Створити програму, що обчислює час, витрачений на обчислення хешу для різного числа ітерацій 
//(10 значень із кроком 50'000; перше значення = номер варіанта * 10'000 ). Побудувати графік залежності витраченого часу від числа ітерацій.


class Program
{
    static void Main()
    {

        Console.WriteLine("List of options you can do:\n1 - hash via HMAC using MD5\n2 - hash via HMAC using SHA1\n3 - hash via HMAC using SHA256\n4 - hash via HMAC using SHA384\n5 - hash via HMAC using SHA512\nChoose what you want to do:");
        int option = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("--------------------------------------------------");
        HashAlgorithmName m = new HashAlgorithmName();

        switch (option)
        {
            case 1:
                m = HashAlgorithmName.MD5;
                Console.WriteLine("Enter the massage to hesh: ");
                string str = Console.ReadLine();
                byte[] messageArray = Encoding.Unicode.GetBytes(str);
                var strmd5 = Convert.ToBase64String(messageArray);
                Console.WriteLine("Password to hash : " + strmd5);
                PBKDF2.Hh5(strmd5, m);
                break;
            case 2:
                m = HashAlgorithmName.SHA1;
                Console.WriteLine("Enter the massage to hesh: ");
                string str2 = Console.ReadLine();
                byte[] messageArray2 = Encoding.Unicode.GetBytes(str2);
                var strsha1 = Convert.ToBase64String(messageArray2);
                Console.WriteLine("Password to hash : " + strsha1);
                PBKDF2.Hh5(strsha1, m);
                break;
            case 3:
                m = HashAlgorithmName.SHA256;
                Console.WriteLine("Enter the massage to hesh: ");
                string str3 = Console.ReadLine();
                byte[] messageArray3 = Encoding.Unicode.GetBytes(str3);
                var strsha256 = Convert.ToBase64String(messageArray3);
                Console.WriteLine("Password to hash : " + strsha256);
                PBKDF2.Hh5(strsha256, m);
                break;
            case 4:
                m = HashAlgorithmName.SHA384;
                Console.WriteLine("Enter the massage to hesh: ");
                string str4 = Console.ReadLine();
                byte[] messageArray4 = Encoding.Unicode.GetBytes(str4);
                var strsha384 = Convert.ToBase64String(messageArray4);
                Console.WriteLine("Password to hash : " + strsha384);
                PBKDF2.Hh5(strsha384, m);
                break;
            case 5:
                m = HashAlgorithmName.SHA512;
                Console.WriteLine("Enter the massage to hesh: ");
                string str5 = Console.ReadLine();
                byte[] messageArray5 = Encoding.Unicode.GetBytes(str5);
                var strsha512 = Convert.ToBase64String(messageArray5);
                Console.WriteLine("Password to hash : " + strsha512);
                PBKDF2.Hh5(strsha512, m);
                break;
        }
    }
}

class PBKDF2
{
    public static byte[] GenerateSalt()
    {
        using (var randomNumberGenerator = new RNGCryptoServiceProvider())
        {
            var randomNumber = new byte[32];
            randomNumberGenerator.GetBytes(randomNumber);
            return randomNumber;
        }
    }
    private static void HashPassword1(string passwordToHash, int numberOfRounds, HashAlgorithmName m)
    {
        var sw = new Stopwatch();
        sw.Start();
        var hashedPassword = PBKDF2.HashPasswordmd5(Encoding.UTF8.GetBytes(passwordToHash), PBKDF2.GenerateSalt(), numberOfRounds, m);
        sw.Stop();
        Console.WriteLine("Hashed Password : " + Convert.ToBase64String(hashedPassword));
        Console.WriteLine("Iterations <" + numberOfRounds + "> Time: " + sw.ElapsedMilliseconds + "ms");
    }

    public static void Hh5(string passwordToHash, HashAlgorithmName m)
    {
        int iter = 50000;
        for (int i = 0; i < 10; i++)
        {
            HashPassword1(passwordToHash, iter, m);
            iter+=50000;
        }

        Console.ReadLine();
    }

    public static byte[] HashPasswordmd5(byte[] toBeHashed, byte[] salt, int numberOfRounds, HashAlgorithmName m)
    {
        Console.WriteLine("");
        using (var rfc2898 = new Rfc2898DeriveBytes(toBeHashed, salt, numberOfRounds, m))
        {
            return rfc2898.GetBytes(16);
        }
    }
}
    /*public static byte[] HashPasswordsha1(byte[] toBeHashed, byte[] salt, int numberOfRounds)
    {
        using (var rfc2898 = new Rfc2898DeriveBytes(toBeHashed, salt, numberOfRounds))
        {
            return rfc2898.GetBytes(20);
        }
    }
    public static byte[] HashPasswordsha256(byte[] toBeHashed, byte[] salt, int numberOfRounds)
    {
        using (var rfc2898 = new Rfc2898DeriveBytes(toBeHashed, salt, numberOfRounds, HashAlgorithmName.SHA256))
        {
            return rfc2898.GetBytes(32);
        }
    }
    public static byte[] HashPasswordsha384(byte[] toBeHashed, byte[] salt, int numberOfRounds)
    {
        using (var rfc2898 = new Rfc2898DeriveBytes(toBeHashed, salt, numberOfRounds, HashAlgorithmName.SHA384))
        {
            return rfc2898.GetBytes(48);
        }
    }
    public static byte[] HashPasswordsha512(byte[] toBeHashed, byte[] salt, int numberOfRounds)
    {
        using (var rfc2898 = new Rfc2898DeriveBytes(toBeHashed, salt, numberOfRounds, HashAlgorithmName.SHA512))
        {
            return rfc2898.GetBytes(64);
        }
    }
}*/



/*
private static void HashPassword2(string passwordToHash, int numberOfRounds)
{
    var sw = new Stopwatch();
    sw.Start();
    var hashedPassword = PBKDF2.HashPasswordsha1(Encoding.UTF8.GetBytes(passwordToHash), PBKDF2.GenerateSalt(), numberOfRounds);
    sw.Stop();
    Console.WriteLine("Hashed Password : " + Convert.ToBase64String(hashedPassword));
    Console.WriteLine("Iterations <" + numberOfRounds + "> Time: " + sw.ElapsedMilliseconds + "ms");
}
private static void HashPassword3(string passwordToHash, int numberOfRounds)
{
    var sw = new Stopwatch();
    sw.Start();
    var hashedPassword = PBKDF2.HashPasswordsha256(Encoding.UTF8.GetBytes(passwordToHash), PBKDF2.GenerateSalt(), numberOfRounds);
    sw.Stop();
    Console.WriteLine("Hashed Password : " + Convert.ToBase64String(hashedPassword));
    Console.WriteLine("Iterations <" + numberOfRounds + "> Time: " + sw.ElapsedMilliseconds + "ms");
}
private static void HashPassword4(string passwordToHash, int numberOfRounds)
{
    var sw = new Stopwatch();
    sw.Start();
    var hashedPassword = PBKDF2.HashPasswordsha384(Encoding.UTF8.GetBytes(passwordToHash), PBKDF2.GenerateSalt(), numberOfRounds);
    sw.Stop();
    Console.WriteLine("Hashed Password : " + Convert.ToBase64String(hashedPassword));
    Console.WriteLine("Iterations <" + numberOfRounds + "> Time: " + sw.ElapsedMilliseconds + "ms");
}
private static void HashPassword5(string passwordToHash, int numberOfRounds)
{
    var sw = new Stopwatch();
    sw.Start();
    var hashedPassword = PBKDF2.HashPasswordsha512(Encoding.UTF8.GetBytes(passwordToHash), PBKDF2.GenerateSalt(), numberOfRounds);
    sw.Stop();
    Console.WriteLine("Hashed Password : " + Convert.ToBase64String(hashedPassword));
    Console.WriteLine("Iterations <" + numberOfRounds + "> Time: " + sw.ElapsedMilliseconds + "ms");
}

public static void Hh1(string passwordToHash)
{
    int iter = 50000;
    for (int i = 0; i < 10; i++)
    {
        HashPassword1(passwordToHash, iter);
        iter+=50000;
    }

    Console.ReadLine();
}

public static void Hh2(string passwordToHash)
{
    int iter = 50000;
    for (int i = 0; i < 10; i++)
    {
        HashPassword2(passwordToHash, iter);
        iter+=50000;
    }

    Console.ReadLine();
}
public static void Hh3(string passwordToHash)
{
    int iter = 50000;
    for (int i = 0; i < 10; i++)
    {
        HashPassword3(passwordToHash, iter);
        iter+=50000;
    }

    Console.ReadLine();
}
public static void Hh4(string passwordToHash)
{
    int iter = 50000;
    for (int i = 0; i < 10; i++)
    {
        HashPassword4(passwordToHash, iter);
        iter+=50000;
    }

    Console.ReadLine();
}*/

//var stopwatch = new Stopwatch();

//stopwatch.Start();
// processing 
//stopwatch.Stop();
//Console.WriteLine($"Iterations number = {iterationsNumber};не Spent time = {stopwatch.ElapsedMilliseconds}");
//stopwatch.Reset();