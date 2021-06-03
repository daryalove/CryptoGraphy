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
    /// Логика взаимодействия для Lab4Page.xaml
    /// </summary>
    public partial class Lab4Page : Page
    {
        public Lab4Page()
        {
            InitializeComponent();
        }

        private long[] open_key;
        private long secure_key;
        private long p;
        private long g;
        private long x;
        private long y;

        string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        /// <summary>
        /// Генерация ключей.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Generate_Keys(object sender, RoutedEventArgs e)
        {
            try
            {
                if (long.TryParse(edit_key.Text, out p))
                {
                    txt_result.Text = "Этап №1 Генерация ключей.\n";

                    var rand = new Random();
                    int k2 = (int)p;
                    g = rand.Next(k2 - 1) + 1;
                    x = rand.Next(k2 - 1) + 1;

                    if (g >= p || x >= p)
                        throw new Exception("Не удалось сгенерировать ключи. Попробуйте снова.");

                    y = ElGamalSharing.Encrypt(g, x, p);

                    open_key = new long[3] { y, g, p };
                    secure_key = x;
                    txt_result.Text += $"g = {g}\n x = {x}\n ";
                    txt_result.Text += $"y = {y}\n";

                    MessageBox.Show("Ключи абонентов успешно сгенерированы!");
                }
                else
                {
                    MessageBox.Show("Ошибка ввода");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            
        }

        /// <summary>
        /// Цифровая подпись сообщения.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Encrypt(object sender, RoutedEventArgs e)
        {
            txt_result.Text += "Этап №2 Преобразование сообщения в числовой эквивалент." + "\n";
            var str = edit_original.Text;
            var m = ElGamalSharing.EncryptLong(str, Alphabet);
            txt_result.Text += $"m = {edit_original.Text} = " + m.ToString() + "\n";

            txt_result.Text += "Этап №3 Подписание сообщения." + "\n";
            int k0 = (int)open_key[2];
            var k = ElGamalSharing.GenerateRandomKey(new Random(), k0);

            txt_result.Text += "k = " + k.ToString() + "\n";
            
            var a = ElGamalSharing.Encrypt(open_key[1], k, open_key[2]);
            txt_result.Text += "a = " + a.ToString() + "\n";
           
            var fdf = m - ((secure_key * a) % (open_key[2] - 1));
            if (fdf < 0)
            {
                fdf += (open_key[2] - 1);
            }
            var b = (fdf * ElGamalSharing.ExtendedEuclid(k, open_key[2] - 1)) % (open_key[2] - 1);
            txt_result.Text += "b = " + b.ToString() + "\n";
            //цифровая подпись 
            var DigitalSignature = new long[2] { a, b };

            txt_result.Text += "Этап №4 Проверка цифровой подписи." + "\n";
            var left = (ElGamalSharing.Encrypt(open_key[0], DigitalSignature[0], open_key[2]) * ElGamalSharing.Encrypt(DigitalSignature[0], DigitalSignature[1], open_key[2])) % open_key[2];
            txt_result.Text += "left = " + left.ToString() + "\n";

            var right = ElGamalSharing.Encrypt(open_key[1], m, open_key[2]);
            txt_result.Text += "right = " + right.ToString() + "\n";
            
            if (left == right)
            {
                txt_result.Text += "Проверка прошла успешно";
            }
            else
            {
                txt_result.Text += "Проверка была пройдена не успешно";
            }
        }
    }
}
