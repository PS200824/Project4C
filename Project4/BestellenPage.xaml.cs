using Project4.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
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

        //__________Pizza__________
        private ObservableCollection<Pizzas> pizzas = new ();
        public ObservableCollection<Pizzas> Pizzas
        {
            get { return pizzas; }
            set { pizzas = value; OnPropertyChanged(); }
        }
        private Pizzas? selectedPizzas;
        public Pizzas? SelectedbPizzas
        {
            get { return selectedPizzas; }
            set { selectedPizzas = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Pizzas> bestellingen = new();

        public ObservableCollection<Pizzas> Bestellingen
        {
            get { return bestellingen; }
            set { bestellingen = value; }
        }

        //__________PizzaGroottes______________
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



        //____________Bestellingen_______________
        private Bestelling bestelling = new();

        public Bestelling Bestelling
        {
            get { return bestelling; }
            set { bestelling = value; }
        }

        private Bestelling selectedbestellingen = new();

        public Bestelling SelectedBestellingen
        {
            get { return selectedbestellingen; }
            set { selectedbestellingen = value; }
        }

        //____________BestelRegel________________
        private ObservableCollection<Bestelregel> bestelregels = new();

        public ObservableCollection<Bestelregel> Bestelregels
        {
            get { return bestelregels; }
            set { bestelregels = value; }
        }

        private Bestelregel? selectedBestelregel; //iets voor cansel
        public Bestelregel? SelectedBestelregel
        {
            get { return selectedBestelregel; }
            set { selectedBestelregel = value; OnPropertyChanged(); ShowUpdatedPrice(); }
        }



        private void ShowUpdatedPrice()
        {
            MessageBox.Show((SelectedbPizzas.Price + SelectedPizzaGrootte.Factor).ToString());
        }


        #endregion
        public BestellenPage()
        {
            InitializeComponent();
            PopulatePizzas();
            PopulatePizzaGroottes();
            PopulateBestelling();
            DataContext = this;
        }

        private void PopulateBestelling()
        {
            string dbResult = db.GetBestellingen(selectedbestellingen, out bestelling);
            if (dbResult != PizzaDB.OK)
            {
                MessageBox.Show(dbResult + serviceDeskBericht);
            }
        }

        private void SaveBestellings()
        {

            string dbResult = db.CreateBestellings(selectedbestellingen);
            if (dbResult != PizzaDB.OK)
            {
                MessageBox.Show(dbResult + serviceDeskBericht);
            }
        }

        private void SaveBestelregels()

        {
            selectedBestelregel = new Bestelregel();
            selectedBestelregel.Pizza = selectedPizzas;
            selectedBestelregel.Aantal = int.Parse(tbAntaal.Text);
            selectedBestelregel.PizzaGrootte= selectedPizzaGrootte;
            PopulateBestelling();
            selectedBestelregel.BestellingId = bestelling.BestellinglId;
            string dbResult = db.CreateBestelRegels(selectedBestelregel);
            if (dbResult != PizzaDB.OK)
            {
                MessageBox.Show(dbResult + serviceDeskBericht);
            }
        }

        private void PopulatePizzaGroottes()
        {
            string dbResult = db.GetPizzaGroottes(PizzaGroottes);
            if (dbResult != PizzaDB.OK)
            {
                MessageBox.Show(dbResult + serviceDeskBericht);
            }
        }

        private void PopulatePizzas()
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
            SaveBestellings();
            SaveBestelregels();
            // caculate price * aantal and + groote
            // save pizzas with price in list
            // show list in listbox

            if (SelectedbPizzas != null)
            {
                Bestellingen.Add(SelectedbPizzas);
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Pizzas teVerwijderenMenu  = btn.DataContext as Pizzas;
            Bestellingen.Remove(teVerwijderenMenu);
            Pizzas.Add(teVerwijderenMenu);


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
