using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Project4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            new MenuPage().Show();
            this.Close();
        }

        private void Bestellen_Click(object sender, RoutedEventArgs e)
        {
            new BestellenPage().Show();
            this.Close();
        }

        private void Annuleren_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
