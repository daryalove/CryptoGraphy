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
    /// Логика взаимодействия для Lab1Page.xaml
    /// </summary>
    public partial class Lab1Page : Page
    {
        public Lab1Page()
        {
            InitializeComponent();
        }

        private void Button_Encrypt_Afin(object sender, RoutedEventArgs e)
        {
            try
            {
                AffineCipher.Run();
                string userString = edit_AfinText.Text;

                int a_key = Convert.ToInt32(edit_a.Text);
                int b_key = Convert.ToInt32(edit_b.Text);

                string cipherText = AffineCipher.Encrypt(userString, a_key, b_key);
                txt_AfinResult.Text = cipherText;
                MessageBox.Show("Успешно!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                txt_AfinResult.Text = ex.Message;
            }
        }

        private void Button_Dencrypt_Afin(object sender, RoutedEventArgs e)
        {
            try
            {
                AffineCipher.Run();
                string userString = edit_AfinText.Text;

                int a_key = Convert.ToInt32(edit_a.Text);
                int b_key = Convert.ToInt32(edit_b.Text);

                string decipherText = AffineCipher.Dencrypt(userString, a_key, b_key);
                txt_AfinResult.Text = decipherText;
                MessageBox.Show("Успешно!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                txt_AfinResult.Text = ex.Message;
            }
        }

        private void Button_Encrypt_Caesar(object sender, RoutedEventArgs e)
        {
            try
            {
                string UserString = edit_original.Text;
                int key = Convert.ToInt32(edit_key.Text);

                if (key < 0 || key > 24)
                {
                    throw new Exception("Введите ключевое число от 0 до 25");
                }
               
                string cipherText = CaesarCipher.Encrypt(UserString, key);
                txt_result.Text = cipherText;
                MessageBox.Show("Успешно!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                txt_result.Text = ex.Message;
            }
        }

        private void Button_Dencrypt_Caesar(object sender, RoutedEventArgs e)
        {
            try
            {
                string UserString = edit_original.Text;
                int key = Convert.ToInt32(edit_key.Text);

                if (key < 0 || key > 24)
                {
                    throw new Exception("Введите ключевое число от 0 до 25");
                }
                string decipherText = CaesarCipher.Dencrypt(UserString, key);
                txt_result.Text = decipherText;
                MessageBox.Show("Успешно!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                txt_result.Text = ex.Message;
            }
        }

        private void Button_Encrypt_Wheat(object sender, RoutedEventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(edit_wheatstone.Text))
                {
                    MessageBox.Show("Введите текст для шифровки/расшифровки", "Ошибка", MessageBoxButton.OK);
                    return;
                }

                string orig_text = edit_wheatstone.Text;
                WheatStoneCipher wheat = new WheatStoneCipher();

                string result_txt = wheat.Encrypt(orig_text);
                txt_WheatResult.Text = result_txt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                txt_WheatResult.Text = ex.Message;
            }
        }

        private void Button_Dencrypt_Wheat(object sender, RoutedEventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(edit_wheatstone.Text))
                {
                    MessageBox.Show("Введите текст для шифровки/расшифровки", "Ошибка", MessageBoxButton.OK);
                    return;
                }

                string orig_text = edit_wheatstone.Text;
                WheatStoneCipher wheat = new WheatStoneCipher();

                string result_txt = wheat.Decrypt(orig_text);
                txt_WheatResult.Text = result_txt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                txt_WheatResult.Text = ex.Message;
            }
        }


        private void Button_Encrypt_Double(object sender, RoutedEventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(edit_double.Text))
                {
                    MessageBox.Show("Введите текст для шифровки/расшифровки", "Ошибка", MessageBoxButton.OK);
                    return;
                }

                string orig_text = edit_double.Text;
                string firstKey = edit_key_word1.Text;
                string secondKey = edit_key_word2.Text;
                
                DoubleTransCipher2 cipher = new DoubleTransCipher2(orig_text, firstKey, secondKey);
                string result_txt = cipher.EncryptDecrypt();
                txt_DoubleResult.Text = result_txt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                txt_DoubleResult.Text = ex.Message;
            }
        }

        private void Button_Decrypt_Double(object sender, RoutedEventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(edit_double.Text))
                {
                    MessageBox.Show("Введите текст для шифровки/расшифровки", "Ошибка", MessageBoxButton.OK);
                    return;
                }

                string orig_text = txt_DoubleResult.Text;
                string firstKey = edit_key_word1.Text;
                string secondKey = edit_key_word2.Text;

                DoubleTransCipher2 cipher = new DoubleTransCipher2(orig_text, firstKey, secondKey);
                string result_txt = cipher.EncryptDecrypt();
                //txt_DoubleResult.Text = result_txt;
                txt_DoubleResult.Text = "система защищена паролем";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                txt_DoubleResult.Text = ex.Message;
            }
        }
    }
}
