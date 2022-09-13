using System;


public class Randomseed
{
    public static void Main()
    {
        Random rnd = new Random(567);
        for (int i = 0; i < 10; i++)
        {
            Console.Write("{0,3} ", rnd.Next(-100, 100));
        }
    }

}
