using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CryptoGraphy.Services
{
    public class AffineCipher
    {
        private static char[] Alphabet;

        public static void Run()
        {
            Alphabet =
                Enumerable.Range('a', 26)
                //.Concat(Enumerable.Range('a', 26))
                //.Concat(Enumerable.Range(' ', 1))
                //.Concat((new int[] { '?', '!', '.', ':', '-', '_', '(', ')' }))
                .Select(x => (char)x)
                .ToArray();
        }

        static int HCF(int a, int b)
        {
            return b == 0 ? a : HCF(b, a % b);
        }

        static bool AreRelativelyPrimes(int m, int n)
        {
            return HCF(m, n) == 1;
        }

        private static bool AssertAB(int a, int b)
        {
            bool result = false;

            string message = string.Empty;

            // a and b must be in the interval 1 <= a <= Alphabet.Length
            if (a < 1 || a > Alphabet.Length)
            {
                message = string.Format("'a' must be in the interval [1,{0}]", Alphabet.Length);
            }

            else if (b < 1 || b > Alphabet.Length)
            {
                message = string.Format("'b' must be in the interval [1,{0}]", Alphabet.Length);
            }

            else if (!AreRelativelyPrimes(a, Alphabet.Length))
            {
                message = string.Format("'a' must be relatively prime to {0}", Alphabet.Length);
            }
            else
            {
                result = true;
            }

            Console.WriteLine(message);

            return result;
        }

        public static string Encrypt(string clearText, int a, int b)
        {
            if (!AssertAB(a, b)) return clearText;

            string result = string.Empty;

            int m = Alphabet.Length;

            foreach (char pChar in clearText)
            {
                if (pChar == 32)
                {
                    result += "_";
                    continue;
                }

                int p = Array.IndexOf(Alphabet, pChar);

                int c = a * p + b % m;
                int cIdx = c % Alphabet.Length;
                char cChar = Alphabet[cIdx];

                result += cChar;
            }

            return result;
        }

        private static int GetMultiplicativeInverse(int a)
        {
            int result = 1;

            for (int i = 1; i <= Alphabet.Length; i++)
            {
                if ((a * i) % (Alphabet.Length) == 1)
                {
                    result = i;
                }
            }

            return result;
        }

        public static string Dencrypt(string cipherText, int a, int b)
        {
            if (!AssertAB(a, b)) return cipherText;

            string result = string.Empty;

            foreach (var cChar in cipherText)
            {
                int c = Array.IndexOf(Alphabet, cChar);

                int aInverse = GetMultiplicativeInverse(a);
                int pIdx = aInverse * (c - b) % Alphabet.Length;
                if (pIdx < 0)
                {
                    pIdx += Alphabet.Length;
                }
                char pChar = Alphabet[pIdx];

                result += pChar;
            }

            return result;
        }
    }
}
