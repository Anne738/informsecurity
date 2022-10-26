using System;
public class Randomseed
{
    public static void Main()
    {
        int seed = 3222;
        int seed1 = 3222;
        int seed2 = 5555;
        int amount = 5;
        int min = 0;
        int max = 100;
        random(seed, amount, min, max);
        random(seed1, amount, min, max);
        random(seed2, amount, min, max);
    }
    public static void random(int seed, int amount, int min, int max)
    {

        Random rnd = new Random(seed);
        Console.WriteLine("Numbers: ");
        for (int i = 0; i < amount; i++)
        {
            Console.WriteLine(rnd.Next(min, max));
        }
    }
}