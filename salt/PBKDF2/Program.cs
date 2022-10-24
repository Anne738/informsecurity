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
                Console.WriteLine("Password to hash : " + str);
                PBKDF2.Hh5(str, m);
                break;
            case 2:
                m = HashAlgorithmName.SHA1;
                Console.WriteLine("Enter the massage to hesh: ");
                string str2 = Console.ReadLine();
                Console.WriteLine("Password to hash : " + str2);
                PBKDF2.Hh5(str2, m);
                break;
            case 3:
                m = HashAlgorithmName.SHA256;
                Console.WriteLine("Enter the massage to hesh: ");
                string str3 = Console.ReadLine();
                Console.WriteLine("Password to hash : " + str3);
                PBKDF2.Hh5(str3, m);
                break;
            case 4:
                m = HashAlgorithmName.SHA384;
                Console.WriteLine("Enter the massage to hesh: ");
                string str4 = Console.ReadLine();
                Console.WriteLine("Password to hash : " + str4);
                PBKDF2.Hh5(str4, m);
                break;
            case 5:
                m = HashAlgorithmName.SHA512;
                Console.WriteLine("Enter the massage to hesh: ");
                string str5 = Console.ReadLine();;
                Console.WriteLine("Password to hash : " + str5);
                PBKDF2.Hh5(str5, m);
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
    public static byte[] HashPasswordmd5(byte[] toBeHashed, byte[] salt, int numberOfRounds, HashAlgorithmName m)
    {
        Console.WriteLine("");
        using (var rfc2898 = new Rfc2898DeriveBytes(toBeHashed, salt, numberOfRounds, m))
        {
            return rfc2898.GetBytes(20);
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
}
