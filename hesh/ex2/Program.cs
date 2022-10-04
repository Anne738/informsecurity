﻿using System;
using System.Security.Cryptography;
using System.Text;



namespace hesh
{
    class Program
    {
        public static void Main()
        {
            String correct = "po1MVkAE7IjUUwu61XxgNg==";
            String attempt = "";

            int counter = 10000000;
            while (attempt!=correct)
            {                
                byte[] messageArray = Encoding.Unicode.GetBytes(counter.ToString());
                messageArray = ComputeHashMd5(messageArray);
                attempt = Convert.ToBase64String(messageArray);
                counter++;
                Console.WriteLine(counter + "         " + attempt);
            }

            static byte[] ComputeHashMd5(byte[] input)
            {
                var md5 = MD5.Create();
                return md5.ComputeHash(input);

            }

        }

    }

}