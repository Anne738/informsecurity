using System;
using System.Collections.Generic;
using System.Security;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Threading;

class Program
{
    static void Main()
    {
        do
        {
            Console.WriteLine("List of options you can do:\n1 - register\n2 - sing in\n3 - exit\nChoose what you want to do:");
            
            switch (Console.ReadLine())
            {
                case "1":
                    Console.WriteLine("\n1 - register 4 users \n2 - register");
                    string c = Console.ReadLine();
                    if (c == "1")
                    {
                        for (int i = 1; i < 5; i++)
                        {
                            Random rnd = new Random();
                            int valuerole = i;


                            if (valuerole == 1)
                            {
                                int numuser = rnd.Next(1, 1000) + 2;
                                string rA = "Admin";
                                string[] role = new[] { rA.ToLower() };
                                var user = Protector.Register("User" + numuser, "PaSsWoRd" + numuser, role);

                                Console.WriteLine("\nLogin: " + user.Login + "\nRoles:" + rA);
                                
                            }
                            else if (valuerole == 2)
                            {
                                int numuser = rnd.Next(1, 1000) + 34;
                                string rS = "Student";
                                string[] role = new[] { rS.ToLower() };
                                var user = Protector.Register("User" + numuser, "PaSsWoRd" + numuser, role);

                                Console.WriteLine("\nLogin: " + user.Login + "\nRoles:" + rS);
                                
                            }
                            else if (valuerole == 3)
                            {
                                int numuser = rnd.Next(1, 1000) + 23;
                                string rT = "Teacher";
                                string[] role = new[] { rT.ToLower() };
                                var user = Protector.Register("User" + numuser, "PaSsWoRd" + numuser, role);

                                Console.WriteLine("\nLogin: " + user.Login + "\nRoles:" + rT);
                                
                            }
                            else if (valuerole == 4)
                            {
                                int numuser = rnd.Next(1, 1000) + 279;
                                string rW = "Worker";
                                string[] role = new[] { rW };
                                var user = Protector.Register("User" + numuser, "PaSsWoRd" + numuser, role);

                                Console.WriteLine("\nLogin: " + user.Login + "\nRoles:" + rW);
                                
                            }
                        }
                        Console.WriteLine("\nDone\n");
                    }
                    else
                    {
                        Console.WriteLine("Choose your role: \nStudent \nTeacher \nWorker \nAdmin");
                        string r = Console.ReadLine();
                        string[] role = new[] { r.ToLower() };
                        Console.WriteLine("Write your login:");
                        string l = Console.ReadLine();
                        Console.WriteLine("Write your password:");
                        string p = Console.ReadLine();
                        Protector.Register(l, p, role);
                        Console.WriteLine("\nDone\n");
                    }
                    break;
                case "2":
                    Console.WriteLine("\n\nLogin:");
                    string login = Console.ReadLine();
                    Console.WriteLine("Password:");
                    string pass = Console.ReadLine();
                    Protector.LogIn(login, pass);
                    //перевірка на роль адміна
                    if (Protector.CheckPassword(login, pass))
                    {
                        try
                        {
                            Protector.OnlyForAdminsFeature();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"{ex.GetType()}: {ex.Message}");
                        }
                    }
                    break;
                case "3":
                    return;
                default:
                    break;
            }

        } while (true);


    }

    public class Protector
    {
        public class User
        {
            public string Login { get; set; }
            public string PasswordHash { get; set; }
            public string Salt { get; set; }
            public string[] Roles { get; set; }
        }
        private static Dictionary<string, User> users = new Dictionary<string, User>();
        public static User Register(string username, string password, string[] roles)
        {
            if (users.ContainsKey(username))
            {
                Console.WriteLine("This username already registered");
                return users[username];
            }
            // генерація солі
            var rng = RandomNumberGenerator.Create();
            var saltBytes = new byte[16];
            rng.GetBytes(saltBytes);
            var saltText = Convert.ToBase64String(saltBytes);
            // rfc
            var hashed_pass = new Rfc2898DeriveBytes(password, saltBytes, 2000);
            var user = new User
            {
                Login = username,
                Salt = saltText,
                PasswordHash = Convert.ToBase64String(hashed_pass.GetBytes(16)),
                Roles = roles
            };
            users.Add(user.Login, user);
            return user;
        }

        public static bool CheckPassword(string username, string password)
        {
            //перевірка логіна у словнику
            if (!users.ContainsKey(username))
            {
                return false;
            }
            var user = users[username];
            byte[] saltBytes = new byte[16];
            saltBytes = Convert.FromBase64String(user.Salt);
            var hashed_pass = new Rfc2898DeriveBytes(password, saltBytes, 2000);
            //співставлення хешів  
            if (Convert.ToBase64String(hashed_pass.GetBytes(16)) != user.PasswordHash)
            {
                return (false);
            }
            return (true);
        }
        public static void LogIn(string userName, string password)
        {
            // Перевірка пароля
            if (CheckPassword(userName, password))
            {
                // Створюється екземпляр автентифікованого користувача
                var identity = new GenericIdentity(userName, "OIBAuth");
                // Виконується прив’язка до ролей, до яких належить користувач
                var principal = new GenericPrincipal(identity, users[userName].Roles);
                // Створений екземпляр автентифікованого користувача з відповідними

                // ролями присвоюється потоку, в якому виконується програма
                System.Threading.Thread.CurrentPrincipal = principal;
            }
            else
            {
                Console.WriteLine("Wrong pass or login");
            }
        }
        public static void OnlyForAdminsFeature()
        {
            // Перевірка того, що потік програми виконується автентифікованим користувачем із певними ролями
            if (Thread.CurrentPrincipal == null)
            {
                throw new SecurityException("Thread.CurrentPrincipal cannot be null.");
            }
            // Перевірка того, що автентифікований користувач належить до ролі "Admins"
            if (!Thread.CurrentPrincipal.IsInRole("admin"))
            {
                throw new SecurityException("User must be a member of Admin to access this feature.");
            }
            // У разі, якщо перевірка пройшла успішно, виконується захищена частина програми
            Console.WriteLine("You have access to this secure feature.");
        }
    }
}