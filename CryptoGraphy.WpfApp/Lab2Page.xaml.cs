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
    /// Логика взаимодействия для Lab2Page.xaml
    /// </summary>
    public partial class Lab2Page : Page
    {
        private long e1;
        private long d1;
        private long e2;
        private long d2;
        private int p;
        private const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public Lab2Page()
        {
            InitializeComponent();
        }

        /// <summary>
        /// передача сообщений
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Encrypt(object sender, RoutedEventArgs e)
        {
            txt_result.Text += "Этап №2 Преобразование сообщения в числовой эквивалент." + "\n";
            var m = ShamirSharing.EncryptLong(edit_original.Text, alphabet);
            txt_result.Text += $"m = {edit_original.Text} = " + m.ToString() + "\n";

            txt_result.Text += "Этап №3 Трехпроходной алгоритм Шамира." + "\n";
            var c1 = ShamirSharing.Encrypt(m, e1, p);
            txt_result.Text += c1.ToString() + " => ";

            var c2 = ShamirSharing.Encrypt(c1, e2, p);
            txt_result.Text += c2.ToString() + " => ";

            var c3 = ShamirSharing.Encrypt(c2, d1, p);
            txt_result.Text += c3.ToString() + " => ";

            m = (long)ShamirSharing.Encrypt(c3, d2, p);
            txt_result.Text += m.ToString() + "\n";

            txt_result.Text += "Этап №4 Преобразование результата\n в исходное сообщение.\n" + 
                $"m = {m} = " + ShamirSharing.DencryptLong(m, alphabet) + "\n";
        }

        /// <summary>
        /// генерация ключей
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Generate_Keys(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(edit_key.Text, out p))
            {
                txt_result.Text = "Этап №1 Генерация ключей.\n";
                var rand = new Random();
                e1 = (long)ShamirSharing.GenerateRandomKey(rand, p);
                d1 = (long)ShamirSharing.ExtendedEuclid(e1, p - 1);

                e2 = (long)ShamirSharing.GenerateRandomKey(rand, p);
                d2 = (long)ShamirSharing.ExtendedEuclid(e2, p - 1);

                txt_result.Text += $" E1 = {e1}   D1 = {d1}\n";
                txt_result.Text += $" E2 = {e2}   D2 = {d2}\r\n";
                MessageBox.Show("Ключи абонентов успешно сгенерированы!");
            }
            else
            {
                MessageBox.Show("Ошибка ввода");
                txt_result.Text += ShamirSharing.Encrypt(10, 5, 23).ToString() + "\r\n";
            }
        }
    }
}
