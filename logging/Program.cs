using NLog;
using System.Security;
using System.Security.Cryptography;
using System.Security.Principal;

class Program
{
    static void Main()
    {
        var logger = NLog.LogManager.GetCurrentClassLogger();

        logger.Trace("do_while loop");
        do
        {
            Console.WriteLine("List of options you can do:\n1 - register\n2 - sing in\n3 - exit\nChoose what you want to do:");
            logger.Trace("swich");
            switch (Console.ReadLine())
            {
                case "1":
                    logger.Debug("swich case 1: register");
                    Console.WriteLine("\n1 - register 4 users \n2 - register");
                    string c = Console.ReadLine();
                    if (c == "1")
                    {
                        logger.Debug("loop if: register 4 users");
                        logger.Trace("Register");
                        for (int i = 1; i < 5; i++)
                        {
                            logger.Info($"Register user: {i}\n");
                            Random rnd = new Random();
                            int valuerole = i;


                            if (valuerole == 1)
                            {
                                int numuser = rnd.Next(1, 1000) + 2;
                                logger.Debug($"loop if for user{numuser}");
                                string rA = "Admin";
                                string[] role = new[] { rA.ToLower() };
                                var user = Protector.Register("User" + numuser, "PaSsWoRd" + numuser, role);

                                Console.WriteLine("\nLogin: " + user.Login + "\nRoles:" + rA + "\n\n");

                            }
                            else if (valuerole == 2)
                            {
                                int numuser = rnd.Next(1, 1000) + 34;
                                logger.Debug($"loop else if for user{numuser}");
                                string rS = "Student";
                                string[] role = new[] { rS.ToLower() };
                                var user = Protector.Register("User" + numuser, "PaSsWoRd" + numuser, role);

                                Console.WriteLine("\nLogin: " + user.Login + "\nRoles:" + rS + "\n\n");

                            }
                            else if (valuerole == 3)
                            {
                                int numuser = rnd.Next(1, 1000) + 23;
                                logger.Debug($"loop else if for user{numuser}");
                                string rT = "Teacher";
                                string[] role = new[] { rT.ToLower() };
                                var user = Protector.Register("User" + numuser, "PaSsWoRd" + numuser, role);

                                Console.WriteLine("\nLogin: " + user.Login + "\nRoles:" + rT + "\n\n");

                            }
                            else if (valuerole == 4)
                            {
                                int numuser = rnd.Next(1, 1000) + 279;
                                logger.Debug($"loop else if for user{numuser}");
                                string rW = "Worker";
                                string[] role = new[] { rW };
                                var user = Protector.Register("User" + numuser, "PaSsWoRd" + numuser, role);

                                Console.WriteLine("\nLogin: " + user.Login + "\nRoles:" + rW + "\n\n");

                            }
                            logger.Debug($"loop for register {"user"+i}");
                        }
                        Console.WriteLine("\nDone\n");
                    }
                    else
                    {
                        logger.Trace("Register");
                        logger.Debug("loop esle: register one user");
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
                    logger.Debug("swich case 2: sing in users");
                    logger.Trace("Sing in");
                    logger.Info("Sing in: user");
                    Console.WriteLine("\n\nLogin:");
                    string login = Console.ReadLine();
                    Console.WriteLine("Password:");
                    string pass = Console.ReadLine();
                    try
                    {
                        Protector.LogIn(login, pass);
                    }
                    catch (WrongPassword ex)
                    {
                        logger.Info("Wrong Credentials");
                        logger.Warn(ex, "Wrong password");
                    }

                    Console.WriteLine("Do you need some information? 1 - YES  2 - NO");
                    int d = Convert.ToInt32(Console.ReadLine());
                    if (d == 1)
                    {
                        logger.Debug("loop if for know some info");
                        if (Protector.CheckPassword(login, pass))
                        {
                            logger.Debug($"loop if: check role admin");
                            logger.Info("User is admin");
                            try
                            {
                                logger.Trace("Only for admins");
                                Protector.OnlyForAdminsFeature();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"{ex.GetType()}: {ex.Message}");
                                logger.Error(ex, "Admins part failed");
                            }
                        }
                    }
                    break;
                case "4":
                    int k, a, b;
                    a = 1; b = 0;
                    try 
                    { 
                        k = a / b; 
                    }
                    catch (DivideByZeroException ex) 
                    {
                        Console.WriteLine($"{ex.GetType()}: {ex.Message}"); 
                        logger.Fatal(ex, "Division by 0."); 
                    }
                    break;
                case "3":
                    logger.Debug("swich case 2: exit");
                    //LogManager.Shutdown();
                    return;
                default:
                    break;
            }
        } while (true);
    }

    public class WrongPassword : System.Exception
    {
        public WrongPassword(string message)
            : base(message) { }
    }
   
    public class Protector
    {
        private static Dictionary<string, User> Users = new Dictionary<string, User>();
        public class User
        {
            public string Login { get; set; }
            public string PasswordHash { get; set; }
            public string Salt { get; set; }
            public string[] Roles { get; set; }
        }
        public static User Register(string username, string password, string[] roles)
        {
            if (Users.ContainsKey(username))
            {
                Console.WriteLine("This username already registered");
                return Users[username];
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
            Users.Add(user.Login, user);
            return user;
        }
        public static bool CheckPassword(string username, string password)
        {
            //перевірка логіна у словнику
            if (!Users.ContainsKey(username))
            {
                return false;
            }
            var user = Users[username];
            var saltBytes = new byte[16];
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
                var principal = new GenericPrincipal(identity, Users[userName].Roles);
                // Створений екземпляр автентифікованого користувача з відповідними
                // ролями присвоюється потоку, в якому виконується програма
                System.Threading.Thread.CurrentPrincipal = principal;
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
                throw new SecurityException("User must be a member of Admins to access this feature.");
            }
            // У разі, якщо перевірка пройшла успішно, виконується захищена частина програми
            Console.WriteLine("You have access to this secure feature.");
        }
    }
}

