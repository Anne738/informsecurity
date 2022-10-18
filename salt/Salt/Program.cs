using System;
using System.Security.Cryptography;
using System.Text;


/*Тема: "Безпечне зберігання паролів"

3. Розробити клас SaltedHash, що реалізує хешування паролів із додаванням додаткової ентропії. Продемонструвати 
роботу класу, обчислюючи хеш для заданого пароля та "солі".

4. Розробити клас PBKDF2, що має наступну функціональність: генерує "сіль", задає алгоритм хешування (MD5, SHA1, SHA256, SHA384, SHA512) 
та обчислює хеш для заданого числа ітерацій. Створити програму, що обчислює час, витрачений на обчислення хешу для різного числа ітерацій 
(10 значень із кроком 50'000; перше значення = номер варіанта * 10'000 ). Побудувати графік залежності витраченого часу від числа ітерацій.

5. Написати програму, що реалізує хешування введеного пароля під час реєстрації користувача та зберігає логін, пароль та "сіль" у пам'яті. 
Реалізувати можливість автентифікації за логіном і паролем. Число ітерацій = номер варіанта * 10'000.*/


class Salt
{
    static void Main()
    {
        const string password = "V3ryC0mpl3xP455w0rd";
        byte[] salt = Hash.GenerateSalt();
        Console.WriteLine("Password : " + password +"\n\n" + "Salt = " + Convert.ToBase64String(salt) + "\n\n" +
            "Hashed Password = " + Convert.ToBase64String(Hash.Hashwithsalt(Encoding.UTF8.GetBytes(password), salt)));
    }
}
public class Hash
{
    public static byte[] GenerateSalt()
    {
        const int saltLength = 32;
        using (var randomNumberGenerator = new RNGCryptoServiceProvider())
        {
            var randomNumber = new byte[saltLength];
            randomNumberGenerator.GetBytes(randomNumber);
            return randomNumber;
        }
    }
    private static byte[] Combine(byte[] pass, byte[] salt)
    {
        var ret = new byte[pass.Length + salt.Length];
        Buffer.BlockCopy(pass, 0, ret, 0, pass.Length);
        Buffer.BlockCopy(salt, 0, ret, pass.Length, salt.Length);
        return ret;
    }
    public static byte[] Hashwithsalt(byte[] input, byte[] salt)
    {
        using (var sha256 = SHA256.Create())
        {
            return sha256.ComputeHash(Combine(input, salt));
        }
    }
}