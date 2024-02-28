using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Symmetrisk_kryptering
{
    internal class DesEncryption
    {
        public static void EncryptDes(string raw)
        { 
            try
            {
                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    byte[] encrypted = Encrypt(raw, des.Key, des.IV);
                    Console.WriteLine($"DES Encrypted message: {Convert.ToBase64String(encrypted)}");
                    string decrypted = Decrypt(encrypted, des.Key, des.IV);
                    Console.WriteLine($"Decrypted data: {decrypted}");
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
        }
        static byte[] Encrypt(string plainText, byte[] Key, byte[] IV)
        {
            byte[] encrypted;
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                ICryptoTransform encryptor = des.CreateEncryptor(Key, IV);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.WriteLine(plainText);
                            sw.Flush();
                        }
                        encrypted = ms.ToArray();
                    }
                }
            }
            return encrypted;
        }

        static string Decrypt(byte[] cipherText, byte[] Key, byte[] IV)
        {
            string plainText;
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider()) 
            {
                ICryptoTransform decryptor = des.CreateDecryptor(Key, IV);
                using (MemoryStream ms = new MemoryStream(cipherText))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(cs))
                        {
                            plainText = sr.ReadToEnd();
                        }
                    }
                }
            }
            return plainText;
        }
    }
}
