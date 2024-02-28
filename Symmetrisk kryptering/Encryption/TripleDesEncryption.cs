using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Symmetrisk_kryptering
{
    internal static class TripleDesEncryption
    {
        public static void EncryptTripleDes(string raw)
        {
            try
            {
                using (TripleDESCryptoServiceProvider tripleDes = new TripleDESCryptoServiceProvider())
                {
                    byte[] encrypted = Encrypt(raw, tripleDes.Key, tripleDes.IV);
                    Console.WriteLine($"Triple DES Encrypted message: {Convert.ToBase64String(encrypted)}");
                    string decrypted = Decrypt(encrypted, tripleDes.Key, tripleDes.IV);
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
            using (TripleDESCryptoServiceProvider tripleDes = new TripleDESCryptoServiceProvider())
            {
                ICryptoTransform encryptor = tripleDes.CreateEncryptor(Key, IV);
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
            using (TripleDESCryptoServiceProvider tripleDes = new TripleDESCryptoServiceProvider())
            {
                ICryptoTransform decryptor = tripleDes.CreateDecryptor(Key, IV);
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
