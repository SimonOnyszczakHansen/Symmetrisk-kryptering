using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Symmetrisk_kryptering
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Vælg en algoritme:\n1. AES\n2. DES\n3. Triple DES\n4. AesGcm");
            var choice = Console.ReadLine();

            if (choice != "1" && choice != "2" && choice != "3" && choice != "4")
            {
                Console.WriteLine("Ugyldigt input");
                Thread.Sleep(2500);
                return;
            }

            Console.WriteLine("Write your message");
            var message = Console.ReadLine();


            switch (choice)
            {
                case "1":
                    AesEncryption.EncryptAes(message);
                    break;
                case "2":
                    DesEncryption.EncryptDes(message);
                    break;
                case "3":
                    TripleDesEncryption.EncryptTripleDes(message);
                    break;
            }
            Console.ReadLine();
        }
    }
}