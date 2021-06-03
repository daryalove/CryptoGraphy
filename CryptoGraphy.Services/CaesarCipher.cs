using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoGraphy.Services
{
    public class CaesarCipher
    {
        public static char Cipher(char ch, int key)
        {
            if (!char.IsLetter(ch))
            {

                return ch;
            }

            char d = char.IsUpper(ch) ? 'A' : 'a';
            return (char)((((ch + key) - d) % 26) + d);
        }

        public static string Encrypt(string input, int key)
        {
            string output = string.Empty;

            foreach (char ch in input)
                output += Cipher(ch, key);

            return output;
        }

        public static string Dencrypt(string input, int key)
        {
            return Encrypt(input, 26 - key);
        }
    }
}
