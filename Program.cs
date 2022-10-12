using System;
using System.Security.Cryptography;
using System.Text;



namespace hesh
{
    class Program
    {
        public static void Main()
        {
            SHA256 sha256 = SHA256.Create();
            var users = new Dictionary<string, byte[]>()
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
                        string login = Console.ReadLine();
                        Console.WriteLine("Create new password: ");
                        byte[] sha_hash = sha256.ComputeHash(Encoding.Unicode.GetBytes(Console.ReadLine()));
                        users.Add(login, sha_hash);
                        Console.WriteLine("registration completed");
                        break;
                    case "2":
                        Console.WriteLine("Enter your login: ");
                        string str1 = Console.ReadLine();
                        bool b = users.ContainsKey(str1);

                        if (b == true)
                        {
                            Console.WriteLine("Password: ");
                            byte[] hash_sha256_2 = sha256.ComputeHash(Encoding.Unicode.GetBytes(Console.ReadLine()));
                            if (users[str1].SequenceEqual(hash_sha256_2))
                            {
                                Console.WriteLine("logged in");
                            }
                            else { Console.WriteLine("wrong password"); }
                        }
                        else { Console.WriteLine("user with this login does not exist"); }
                        break;
                    case "3":
                        return;
                    default:
                        break;
                }
            } while (true);
        }
    }
}

