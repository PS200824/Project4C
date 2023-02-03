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
        private ObservableCollection<Bestelregel> bestelregels = new ObservableCollection<Bestelregel>();
        public ObservableCollection<Bestelregel> Bestelregels
        {
            get { return bestelregels; }
            set { bestelregels = value; OnPropertyChanged(); }
        }
        private Pizzas? selectedPizza;
        public Pizzas? SelectedPizza
        {
            get { return selectedPizza; }
            set { selectedPizza = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Pizzas> bestellingen = new();

        public ObservableCollection<Pizzas> Bestellingen
        {
            get { return bestellingen; }
            set { bestellingen = value; }
        }


        private ObservableCollection<PizzaGrootte> pizzaGroottes= new();

        public ObservableCollection<PizzaGrootte> PizzaGroottes
        {
            get { return pizzaGroottes; }
            set { pizzaGroottes = value; }
        }

        private PizzaGrootte? selectedPizzaGrootte; //iets voor cansel
        public PizzaGrootte? SelectedPizzaGrootte
        {
            get { return selectedPizzaGrootte; }
            set { selectedPizzaGrootte = value; OnPropertyChanged(); ShowUpdatedPrice(); }
        }

        private void ShowUpdatedPrice()
        {
            MessageBox.Show((SelectedPizza.Price * SelectedPizzaGrootte.Factor).ToString());
        }


        #endregion
        public BestellenPage()
        {
            InitializeComponent();
            PopulateMenus();
            PopulatePizzaGroottes();
            DataContext = this;
    }

        private void PopulatePizzaGroottes()
        {
            string dbResult = db.GetPizzaGroottes(PizzaGroottes);
            if (dbResult != PizzaDB.OK)
            {
                MessageBox.Show(dbResult + serviceDeskBericht);
            }
        }

        private void PopulateMenus()
        {
            string dbResult = db.GetPizza(Pizzas);
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
            if (SelectedPizza != null)
            {
                Bestellingen.Add(SelectedPizza);

            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Pizzas teVerwijderenMenu  = btn.DataContext as Pizzas;
            PizzaGrootte teVerwijderenMenu2 = btn.DataContext as PizzaGrootte;
            Bestellingen.Remove(teVerwijderenMenu);
            Pizzas.Add(teVerwijderenMenu);
            PizzaGroottes.Add(teVerwijderenMenu2);


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
            new BestellenStatusPage().Show();
            this.Close();
        }

        double total = 0;



        //TOTAAL PRIJS BEREKENEN

        //private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        //{
            //if (comboBox1.SelectedItem.ToString() == "Item 1")
            //{
            //    total += 10;
            //}
            //else if (comboBox1.SelectedItem.ToString() == "Item 2")
            //{
             //   total += 20;
            //}
            //else if (comboBox1.SelectedItem.ToString() == "Item 3")
            //{
              //  total += 30;
            //}

            //labelTotal.Text = "Total: $" + total.ToString();
        //}







    }
}
