using System.Security.Cryptography;
class Rancryp
{
    public static void Main()
    {
        for (int i = 0; i < 10; i++)
        {
            Console.WriteLine(Convert.ToBase64String(Random.GeneratorRndNum(64)));
        }
    }
}
public class Random
{
    public static byte[] GeneratorRndNum(int bit)
    {
        using (var rnd = new RNGCryptoServiceProvider())
        {
            var rndNum = new byte[bit];
            rnd.GetBytes(rndNum);
            int Numb = BitConverter.ToInt32(rndNum, 0);
            Console.WriteLine(Numb);
            return rndNum;
        }
    }
}