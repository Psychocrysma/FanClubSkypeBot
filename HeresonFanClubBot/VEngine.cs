using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CryptorEngine
{
    public class VEngine
    {
        static char[,] tableau = fillArray();

        public static string Encrypt(string plainText, string key)
        {
            string result = "";
            // Uppercase and trim our strings
            key = key.Trim().ToUpper();
            plainText = plainText.Trim().ToUpper();
            int keyIndex = 0;
            int keylength = key.Length;
            if ((keylength <= 0)) { return result; }
            for (int i = 0; i < plainText.Length; i++)
            {
                // Remove anything that isn't a letter.
                if (Char.IsLetter(plainText, i))
                {
                    // This applies the key to each letter, recycling the key once at its end.
                    keyIndex = keyIndex % keylength;
                    result += LookupLetter(key.Substring(keyIndex, 1), plainText.Substring(i, 1));
                    keyIndex++;
                }
            }
            return result;
        }

        public static string Decrypt(string cipherText, string key)
        {
            string result = "";
            // Capitalize and trim string values
            key = key.Trim().ToUpper(); ;
            cipherText = cipherText.Trim().ToUpper();
            int keyIndex = 0;
            int keylength = key.Length;
            if (keylength <= 0) { return result; }
            // Loop through cipher text, pulling each character
            for (int i = 0; i < cipherText.Length; i++)
            {
                // If letter, offset it and then search that column for the cipher chracter
                if (Char.IsLetter(cipherText, i))
                {
                    keyIndex = keyIndex % keylength;
                    int keyChar = Convert.ToInt32(Convert.ToChar(key.Substring(keyIndex, 1))) - 65;
                    // If we found the cipher character in the column
                    // Reapply offset and convert to original uncrypted letter index
                    for (int find = 0; find < 26; find++)
                    {
                        if (tableau[keyChar, find].ToString().CompareTo(cipherText.Substring(i, 1)) == 0)
                        {
                            result += (char)(find + 65);
                        }
                    }
                    keyIndex++;
                }
            }
            return result;
        }

        public static char[,] fillArray()
        {
            string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            int length = letters.Length;
            tableau = new char[length, length];
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    // Shift our letters once each row
                    int getLetter = j + i;
                    // If we reached the end of the letters, start at the beginning again.
                    if (getLetter > (length - 1)) { getLetter -= length; }
                    tableau[i, j] = Convert.ToChar(letters.Substring(getLetter, 1));
                }
            }
            return tableau;
        }

        public static char LookupLetter(string character1, string character2)
        {
            int letter1 = Convert.ToInt32(Convert.ToChar(character1));
            int letter2 = Convert.ToInt32(Convert.ToChar(character2));
            // Offset capital letters by 65 (value of 'A') to give us an index value
            letter1 -= 65;
            letter2 -= 65;
            // Check to make sure value is in given alphabet range.
            if ((letter1 >= 0) && (letter1 <= 26))
            {
                if ((letter2 >= 0) && (letter2 <= 26))
                {
                    return tableau[letter1, letter2];
                }
            }
            return ' ';
        }
    }
}
