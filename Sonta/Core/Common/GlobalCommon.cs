using System;
using System.ComponentModel;
using System.IO;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Core.Common
{
    public class GlobalCommon
    {
        private const string initVector = "pemgail9uzpgzl88";
        private const int keysize = 256;

        public static string CreateRandomPassword(int length = 9)
        {
            // Create a string of characters, numbers, special characters that allowed in the password  
            string validChars = "abcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();

            // Select one random character at a time from the string  
            // and create an array of chars  
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            return new string(chars);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public static string EncryptString(string plainText)
        {
            try
            {
                if (string.IsNullOrEmpty(plainText)) return plainText;

                byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
                PasswordDeriveBytes password = new PasswordDeriveBytes(initVector, null);
                byte[] keyBytes = password.GetBytes(keysize / 8);
                RijndaelManaged symmetricKey = new RijndaelManaged();
                symmetricKey.Mode = CipherMode.CBC;
                ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                cryptoStream.FlushFinalBlock();
                byte[] cipherTextBytes = memoryStream.ToArray();
                memoryStream.Close();
                cryptoStream.Close();
                return Convert.ToBase64String(cipherTextBytes);
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cipherText"></param>
        /// <returns></returns>
        public static string DecryptString(string cipherText)
        {
            try
            {
                if (string.IsNullOrEmpty(cipherText)) return cipherText;
                cipherText = cipherText.Replace(" ", "+");

                byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
                byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
                PasswordDeriveBytes password = new PasswordDeriveBytes(initVector, null);
                byte[] keyBytes = password.GetBytes(keysize / 8);
                RijndaelManaged symmetricKey = new RijndaelManaged();
                symmetricKey.Mode = CipherMode.CBC;
                ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
                MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
                CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
                byte[] plainTextBytes = new byte[cipherTextBytes.Length];
                int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                memoryStream.Close();
                cryptoStream.Close();
                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static T ConvertTo<T>(object obj)
        {
            T objDes = Activator.CreateInstance<T>();
            var properties = typeof(T).GetProperties();

            foreach (var i in properties)
            {
                var converter = TypeDescriptor.GetConverter(i.PropertyType);

                try
                {
                    var t = obj.GetType().GetProperty(i.Name);
                    if (t != null && t.GetValue(obj) != null)
                    {
                        //var convertedObject = converter.ConvertFrom(t.GetValue(obj));
                        var convertedObject = Convert.ChangeType(t.GetValue(obj), Nullable.GetUnderlyingType(i.PropertyType)
                            ?? i.PropertyType);
                        objDes.GetType().GetProperty(i.Name).SetValue(objDes, convertedObject, null);
                    }
                }
                catch (Exception ex)
                {
                    string str = ex.Message;
                }
            }

            return objDes;
        }
    }
}
