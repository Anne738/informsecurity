using System;
using System.Security.Cryptography;
using System.Text;
using System.Diagnostics;


// 5. Написати програму, що реалізує хешування введеного пароля
// під час реєстрації користувача та зберігає логін, пароль та "сіль" у пам'яті.
// Реалізувати можливість автентифікації за логіном і паролем. Число ітерацій = номер варіанта * 10'000.

class Program
{
    public static void Main()
    {
        string[] login = new string[10];
        string[] password = new string[10];
        string[] salt = new string[10];
        int num = 0;
        { };
        do
        {
            byte[] data = null;
            Console.WriteLine("List of options you can do:\n1 - register\n2 - login\n3 - exit\nChoose what you want to do:");
            Console.WriteLine("--------------------------------------------------");

            switch (Console.ReadLine())
            {
                case "1":
                    Console.WriteLine("Enter new login: ");
                    string log = Console.ReadLine();
                    login[num] = log;
                    Console.WriteLine("Create new password: ");
                    byte[] s = reg.GenerateSalt();
                    salt[num] = Convert.ToBase64String(s);
                    byte[] pass = reg.HashPasswordsha1(Encoding.Unicode.GetBytes(Console.ReadLine()), s, 50000);
                    password[num] = Convert.ToBase64String(pass);
                    num++;
                    Console.WriteLine("registration completed");
                    break;
                case "2":
                    Console.WriteLine("Enter your login: ");
                    string log_2 = Console.ReadLine();
                    Console.WriteLine("Password: ");
                    int num_login = Array.IndexOf(login, log_2); 
                    byte[] pass_h = reg.HashPasswordsha1(Encoding.Unicode.GetBytes(Console.ReadLine()), Convert.FromBase64String(salt[num_login]) , 50000); 
                    if (password[num_login] == Convert.ToBase64String(pass_h))
                    {
                        Console.WriteLine("done");
                    }
                    else { Console.WriteLine("user does not exist"); }
                    break;
                case "3":
                    return;
                default:
                    break;
            }
        } while (true);
    }
}
    
class reg
{ 
    public static byte[] HashPasswordsha1(byte[] toBeHashed, byte[] salt, int numberOfRounds)
    {
        using (var rfc2898 = new Rfc2898DeriveBytes(toBeHashed, salt, numberOfRounds))
        {
            return rfc2898.GetBytes(20);
        }
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
}

