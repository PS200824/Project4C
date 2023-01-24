using Project4.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for BestellenPage.xaml
    /// </summary>
    public partial class BestellenPage : Window , INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
        #region fields
        private readonly PizzaDB db = new PizzaDB();
        private readonly string serviceDeskBericht = "\n\nNeem contact op met de service desk";
        #endregion
        #region Properties
        private ObservableCollection<Menus> menus = new ObservableCollection<Menus>();
        public ObservableCollection<Menus> Menus
        {
            get { return menus; }
            set { menus = value; OnPropertyChanged(); }
        }
        private Menus? selectedMenu;
        public Menus? SelectedMenu
        {
            get { return selectedMenu; }
            set { selectedMenu = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Menus> bestellingen = new ();

        public ObservableCollection<Menus> Bestellingen
        {
            get { return bestellingen;  }
            set { bestellingen = value; }
        }

        #endregion
        public BestellenPage()
        {
            InitializeComponent();
            PopulateMenus();
            DataContext = this;
    }

        private void PopulateMenus()
        {
            string dbResult = db.GetMenu(Menus);
            if (dbResult != PizzaDB.OK)
            {
                MessageBox.Show(dbResult + serviceDeskBericht);
            }
        }

        private void PizzaMenu_Click(object sender, RoutedEventArgs e)
        {
            new MenuPage().Show();
            this.Close();
        }

        private void Annuleren_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        private void AddPizza_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedMenu != null)
            {
                Bestellingen.Add(SelectedMenu);
                Menus.Remove(SelectedMenu);

            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Menus teVerwijderenMenu  = btn.DataContext as Menus;
            Bestellingen.Remove(teVerwijderenMenu);
            Menus.Add(teVerwijderenMenu);


        }

        private void btnBestellen_Click(object sender, RoutedEventArgs e)
        {
            if (BestelBox.Items.Count == 0)
            {
                MessageBox.Show("niks geselecteerd");
            }
            else
            {
                MessageBox.Show("Bestellen van pizza is gelukt");
            }

        }

        private void btnBestellenStatus_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AntaalPizza_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
