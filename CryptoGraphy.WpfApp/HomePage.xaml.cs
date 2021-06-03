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
    /// Логика взаимодействия для HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void Click_1(object sender, RoutedEventArgs e)
        {
            // View Page
            Lab1Page expenseReportPage = new Lab1Page();
            this.NavigationService.Navigate(expenseReportPage);
        }

        private void Click_2(object sender, RoutedEventArgs e)
        {
            // View Page
            Lab2Page expenseReportPage = new Lab2Page();
            this.NavigationService.Navigate(expenseReportPage);
        }

        private void Click_3(object sender, RoutedEventArgs e)
        {
            // View Page
            Lab3Page expenseReportPage = new Lab3Page();
            this.NavigationService.Navigate(expenseReportPage);
        }

        private void Click_4(object sender, RoutedEventArgs e)
        {
            // View Page
            Lab4Page expenseReportPage = new Lab4Page();
            this.NavigationService.Navigate(expenseReportPage);
        }
    }
}
