using System;
using System.Security.Cryptography;
using System.Text;


struct User{
    public string Login;
    public string Password;
}


namespace hesh
{
    class Program
    {
        public static void Main()
        {
            byte[] data = null;
            string massage = null;
            Console.WriteLine("List of options you can do:");
            Console.WriteLine("1 - register");
            Console.WriteLine("2 - login");
            Console.WriteLine("3 - exit");
            Console.WriteLine("Choose what you want to do:");
            int option = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("--------------------------------------------------");
            do
            {
                switch (option)
                {
                    case 1:
                        Console.WriteLine("Enter new login: ");
                        string login = Console.ReadLine();
                        byte[] l = Encoding.Unicode.GetBytes(login);
                        Console.WriteLine("Create new password: ");
                        string password = Console.ReadLine();
                        byte[] p = Encoding.Unicode.GetBytes(password);
                        var sha256 = ComputeHashSha256(l, p);
                        data = sha256;
                        Console.WriteLine("Enter info");
                        massage = Console.ReadLine();
                        break; 
                    case 2:
                        Console.WriteLine("Enter your login: ");
                        string str1 = Console.ReadLine();
                        byte[] log = Encoding.Unicode.GetBytes(str1);
                        Console.WriteLine("Password: ");
                        string pass = Console.ReadLine();
                        byte[] pa = Encoding.Unicode.GetBytes(pass);
                        var sha256_2 = ComputeHashSha256(log, pa);
                        if (data == sha256_2)
                            Console.WriteLine(massage);
                        else Console.WriteLine("wrong login or password");
                        break;
                    case 3:
                        break;
                }
            } while (true);
            
            static byte[] ComputeHashSha256(byte[] input, byte[] key)
            {
                using (var sha256 = new HMACSHA256())
                {
                    return sha256.ComputeHash(input);
                }
            }
        }
    }
}

                    