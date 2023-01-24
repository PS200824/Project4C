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
using System.Windows.Shapes;

namespace Project4
{
    /// <summary>
    /// Interaction logic for BestellenStatusPage.xaml
    /// </summary>
    public partial class BestellenStatusPage : Window
    {
        public BestellenStatusPage()
        {
            InitializeComponent();
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            /*this.Close();*/
            mainWindow.ShowDialog();
            /*new WinCustomer().Show();*/
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            MenuPage menuPage = new MenuPage();
            /*this.Close();*/
            menuPage.ShowDialog();
            /*new WinCustomer().Show();*/
        }

        private void Annuleren_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
