using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CryptoGraphy.Services
{
    public static class ShamirSharing
    {
        //генерация ключа
        public static int GenerateRandomKey(Random rand, int p)
        {
            int a;
            do
            {
                a = (rand.Next(p)) + (p / 2);
                if (a % 2 == 0)
                {
                    a++;
                }
            }
            while (euclid(a, p - 1) != 1);
            return a;
        }

        //алгоритм Эвклида
        private static long euclid(long a, long b)
        {
            while (b != 0)
            {
                var c = a % b;
                a = b;
                b = c;
            }
            return a;
        }

        //расширенный алгоритм Эвклида
        public static long ExtendedEuclid(long a, long b)
        {
            var p = b;
            long x0 = 1;
            long x1 = 0;
            while (b != 1)
            {
                var d = (long)Math.Truncate((double)a / (double)b);
                var c = a - d * b;
                var x2 = x0 - ((long)d * x1);
                a = b;
                b = c;
                x0 = x1;
                x1 = x2;
            }
            if (x1 < 0)
            {
                x1 += p;
            }
            return x1;
        }

        //представление десятичного числа в двоичный формат
        private static string binary_encrypt(long number)
        {
            if (number == 1)
                return "1";
            else
                return binary_encrypt(number / 2) + (number % 2);
        }

        //шифрование сообщения методом повторного возведения в квадрат
        public static long Encrypt(long m, long e, long p)
        {
            var strlong = binary_encrypt((long)e).Reverse().ToArray();
            var b = m % p;
            long a;
            if (strlong[0] == '1')
            {
                a = b;
            }
            else
            {
                a = b;
            }
            for (var i = 1; i < strlong.Length; i++)
            {
                b = (long)(((ulong)b * (ulong)b) % (ulong)p);
                if (strlong[i] == '1')
                {
                    a = (long)(((ulong)a * (ulong)b) % (ulong)p);
                }
            }
            return a;
        }

        //преобразование сообщения в числовой формат
        public static long EncryptLong(string m, string alph)
        {
            long a = 0;
            int a_base = alph.Length + 1;

            for (var i = 0; i < m.Length; i++)
            {
                for (var j = 0; j < alph.Length; j++)
                {
                    if (m[i] == alph[j])
                    {
                        a += (j + 1) * (long)Math.Pow(a_base, m.Length - 1 - i);
                    }
                }
            }
            return a;
        }

        //преобразование результата в сообщение
        public static string DencryptLong(long m, string alph)
        {
            string str = "";
            int a_base = alph.Length + 1;

            while (m > 0)
            {
                var c = m % a_base;
                str = alph[(int)(c - 1)] + str;
                m /= a_base;
            }
            return str;
        }
    }
}
