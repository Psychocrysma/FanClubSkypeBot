using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CryptorEngine
{
    public class CryptorEngine
    {
        public static string EncryptAB(string value)
        {
            char[] _shift = new char[char.MaxValue];
            // Set these as the same.
            for (int i = 0; i < char.MaxValue; i++)
            {
                _shift[i] = (char)i;
            }
            // Reverse order of capital letters.
            for (char c = 'A'; c <= 'Z'; c++)
            {
                _shift[(int)c] = (char)('Z' + 'A' - c);
            }
            // Reverse order of lowercase letters.
            for (char c = 'a'; c <= 'z'; c++)
            {
                _shift[(int)c] = (char)('z' + 'a' - c);
            }
            // Convert to char array
            char[] a = value.ToCharArray();
            // Shift each letter.
            for (int i = 0; i < a.Length; i++)
            {
                int t = (int)a[i];
                a[i] = _shift[t];
            }
            // Return new string.
            return new string(a);
        }

        public static string reverse(string plainIn)
        {
            char[] charArray = plainIn.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        private const int LETTERA = (int)'A';
        private const int LETTERS = (int)'Z' - LETTERA + 1;

        public static string shift(string plainIn, int shift)
        {
            // 65=A 90=Z in ASCII
            string readOut = string.Empty;
            char[] charArray;

            charArray = plainIn.ToCharArray();
            for (int i = 0; i < charArray.Length; i++)
            {
                readOut += (char)(LETTERA + (((int)charArray[i] + shift - LETTERA + LETTERS) % LETTERS));
            }
            return readOut;
        }

        public static string EncryptC(string ReadIn, int Shift)
        {
            ReadIn = reverse(ReadIn);
            ReadIn = shift(ReadIn, Shift);
            return ReadIn;
        }

        public static string DecryptC(string ReadIn, int amountShift)
        {
            ReadIn = reverse(ReadIn);
            ReadIn = shift(ReadIn, -amountShift);
            return ReadIn;
        } 

        public static string Encrypt(string toEncrypt, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();
            // Get the key from config file
            string key = (string)settingsReader.GetValue("SecurityKey", typeof(String));
            //System.Windows.Forms.MessageBox.Show(key);
            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            tdes.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        /// <summary>
        /// DeCrypt a string using dual encryption method. Return a DeCrypted clear string
        /// </summary>
        /// <param name="cipherString">encrypted string</param>
        /// <param name="useHashing">Did you use hashing to encrypt this data? pass true is yes</param>
        /// <returns></returns>
        public static string Decrypt(string cipherString, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();
            //Get your key from config file to open the lock!
            string key = (string)settingsReader.GetValue("SecurityKey", typeof(String));
            
            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                        
            tdes.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
    }
}
