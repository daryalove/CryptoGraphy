using CryptoGraphy.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CryptoGraphy.WpfApp
{
    /// <summary>
    /// Логика взаимодействия для Lab3Page.xaml
    /// </summary>
    public partial class Lab3Page : Page
    {
        private long[] OpenKey;
        private long d;
        private long p;
        private long q;
        private long n;
        private long fn;
        private long E;
        private const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public Lab3Page()
        {
            InitializeComponent();
        }

        /// <summary>
        /// генерация ключей
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Generate_Keys(object sender, RoutedEventArgs e)
        {
            if (long.TryParse(edit_key_p.Text, out p) && long.TryParse(edit_key_q.Text, out q))
            {
                txt_result.Text = "Этап №1 Генерация ключей.\n";
                n = p * q;

                fn = (p - 1) * (q - 1);
                E = 65537;

                OpenKey = new long[2] { E, n };
                d = ShamirSharing.ExtendedEuclid(E, fn);

                txt_result.Text += $"e = {E}   n = {n}\n ";
                txt_result.Text += $"fn = {fn}   d = {d}\r\n";
                MessageBox.Show("Ключи абонентов успешно сгенерированы!");
            }
            else
            {
                MessageBox.Show("Ошибка ввода");
                txt_result.Text += ShamirSharing.DencryptLong(301941131, alphabet).ToString() + "\r\n";
            }
        }

        /// <summary>
        /// передача сообщений
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Encrypt(object sender, RoutedEventArgs e)
        {
            //txt_result.Text += "Этап №2 Преобразование числа в числовой эквивалент." + "\n";
            //var str = ATextBox.Text;
            //Other.Text = str + "\n";
            //var m = Class1.Cryptionlong(str, Alphabet);
            //Other.Text += m.ToString() + "\n";
            //var c1 = Class1.Cryption(m, OpenKey[0], OpenKey[1]);
            //Other.Text += c1.ToString() + "\n";
            //str = Class1.DeCryptionlong(c1, Alphabet);
            //Other.Text += str + "\n";
            /////////////передача сообщения 
            //m = Class1.Cryptionlong(str, Alphabet);
            //Other.Text += m.ToString() + "\n";
            //var c2 = Class1.Cryption(m, d, OpenKey[1]);
            //Other.Text += c2.ToString() + "\n";
            //str = Class1.DeCryptionlong(c2, Alphabet);
            //Other.Text += str + "\n";
            //BTextBox.Text = str;


            txt_result.Text += "Этап №2 Преобразование открытого текста\n в числовой эквивалент." + "\n";
            var m = ShamirSharing.EncryptLong(edit_original.Text, alphabet);
            txt_result.Text += $"m = {edit_original.Text} = " + m.ToString() + "\n";

            txt_result.Text += "Этап №3 Шифрование." + "\n";
            var c = ShamirSharing.Encrypt(m, OpenKey[0], OpenKey[1]);
            txt_result.Text += $"c = {c}" + "\n";

            txt_result.Text += "Этап №4 Преобразование шифртекста\n в символьное представление." + "\n";
            var c1 = ShamirSharing.DencryptLong(c, alphabet);
            txt_result.Text += $"c = {c1}" + "\n";

            //передача сообщения
            txt_result.Text += "Этап №5 Преобразование символьного представления\n шифртекста в числовой эквивалент." + "\n";
            m = ShamirSharing.EncryptLong(c1, alphabet);
            txt_result.Text += $"m = {c1} = " + m.ToString() + "\n";
            
            txt_result.Text += "Этап №6 Расшифровка сообщения." + "\n";
            var c2 = ShamirSharing.Encrypt(m, d, OpenKey[1]);
            txt_result.Text += $"m = {c2}" + "\n";

            txt_result.Text += "Этап №7 Восстановление символьного представления\n открытого текста." + "\n";
            var result = ShamirSharing.DencryptLong(c2, alphabet);
            txt_result.Text += $"m = {result}" + "\n";
        }

        private void GroupBox_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }
    }
}
