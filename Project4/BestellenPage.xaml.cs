using Google.Protobuf.WellKnownTypes;
using Project4.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Reflection.Metadata.Ecma335;
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
        private Bestelregel bestelregels = new ();
        private Bestelregel Bestelregels
        {
            get { return bestelregels; }
            set { bestelregels = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Pizza> pizza = new ();
        public ObservableCollection<Pizza> Pizza
        {
            get { return pizza; }
            set { pizza = value; OnPropertyChanged(); }
        }
        private Pizza? selectePizza;
        public Pizza? SelectePizza
        {
            get { return selectePizza; }
            set { selectePizza = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Pizza> bestellingen = new();

        public ObservableCollection<Pizza> Bestellingen
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
            set { selectedPizzaGrootte = value; OnPropertyChanged(); }
        }



        //____________Bestellingen_______________
        private Bestelling bestelling = new Bestelling();

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
        private ObservableCollection<Bestelregel> bestelregel = new();

        public ObservableCollection<Bestelregel> Bestelregel
        {
            get { return bestelregel; }
            set { bestelregel = value; }
        }

        private Bestelregel? selectedBestelregel; //iets voor cansel
        public Bestelregel? SelectedBestelregel { 
            get { return selectedBestelregel; }
            set { selectedBestelregel = value; OnPropertyChanged(); }
        }



        private decimal bestelRegelPrice()
        {
            return (SelectePizza.Price + selectedPizzaGrootte.Factor);
        }


        #endregion
        public BestellenPage()
        {
            InitializeComponent();
            PopulatePizzas();
            PopulatePizzaGroottes();
            //PopulateBestellingen();
            //
            DataContext = this;
        }

        private void PopulateBestelregel()
        {
            string dbResult = db.GetBestelregels(bestelregel, bestelling);
            if (dbResult != PizzaDB.OK)
            {
                MessageBox.Show(dbResult + serviceDeskBericht);
            }
        }

        private void PopulateBestellingen()
        {
            string dbResult = db.GetBestellingen
            (selectedbestellingen, out bestelling);
            if (dbResult != PizzaDB.OK)
            {
               MessageBox.Show(dbResult + serviceDeskBericht);
            }
        }

        //add bestelling
        private void SaveBestellings(Bestelling bestelling)
        {
            string dbResult = db.CreateBestellings(bestelling);  
                if (dbResult != PizzaDB.OK)
            {
                MessageBox.Show(dbResult + serviceDeskBericht);
            }
        }

        private void UpdateStatus(int bestellingId)
        {
            string dbResult = db.UpdateStatus(bestellingId, bestelling);
            if (dbResult != PizzaDB.OK)
            {
                MessageBox.Show(dbResult + serviceDeskBericht);
            }
        }
        public void DeleteBestelregel(int bestelregelId)
        {
            string dbResult = db.DeleteBestelregel(bestelregelId);
            if (dbResult != PizzaDB.OK)
            {
                MessageBox.Show(dbResult + serviceDeskBericht);
            }
        }

        private void SaveBestelregels()

        {
            selectedBestelregel = new Bestelregel();
            selectedBestelregel.Pizza = SelectePizza;
            selectedBestelregel.Aantal = int.Parse(tbAntaal.Text);
            selectedBestelregel.PizzaGrootte= selectedPizzaGrootte;
            PopulateBestellingen();               
            selectedBestelregel.BestellingId = bestelling.BestellinglId;
            string dbResult = db.CreateBestelRegels(selectedBestelregel);
            if (dbResult != PizzaDB.OK)
            {
                MessageBox.Show(dbResult + serviceDeskBericht);
            }
            //selectedBestelregel.Regelprijs = bestelRegelPrice();
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
            string dbResult = db.GetPizza(Pizza);
            if (dbResult != PizzaDB.OK)
            {
                MessageBox.Show(dbResult + serviceDeskBericht);
            }
        }

        //Windows Button
        private void PizzaMenu_Click(object sender, RoutedEventArgs e)
        {
            new MenuPage().Show();
            this.Close();
        }

        private void Annuleren_Click(object sender, RoutedEventArgs e)
        {
            //If the button where clicked then showed messageBox 
            this.Close();
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }


        private void AddPizza_Click(object sender, RoutedEventArgs e)
        {
            
            if (tbAntaal.Text == "" )
            {
                MessageBox.Show("Voeg een aantal toe");
                return;
            }
            if (cmPizzaGroot.SelectedItem == null )
            {
                MessageBox.Show("Voeg een de Pizza groot toe");
                return;
            }
            if (cmPizzaGroot.Items[1] == string.Empty == null)
            {
                MessageBox.Show("select een Pizza");
                return;
            }

            if (bestelling.BestellinglId == 0) SaveBestellings(bestelling);
                SaveBestelregels();

            if (SelectePizza != null)
            {
                bestelregel.Clear();
                PopulateBestelregel();
            }

            totalprijs.Text = bestelling.Bestellingprijs().ToString("€ 0.00"); //bestelregel totaal prijs komt hier (CANSEL)
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Bestelregel teVerwijderenBestelregel  = btn.DataContext as Bestelregel;
            DeleteBestelregel(teVerwijderenBestelregel.BestelregelId);
            MessageBox.Show("Deleted");
            bestelregel.Clear();
            PopulateBestelregel();
            totalprijs.Text = bestelling.Bestellingprijs().ToString("€ 0.00"); //totaalprijs textbox zodat hier niet nog steeds oude prijs blijft (CANSEL)

        }

        private void btnBestellen_Click(object sender, RoutedEventArgs e)
        {
            bestelling.Status = true;
            bestelling.Besteldatum = DateTime.Now;
            UpdateStatus(bestelling.BestellinglId);
            if (bestelling.Status == true)
            {
                MessageBox.Show("Bestellen van pizza is gelukt");
                bestelling.Besteldatum = DateTime.Now;
                AddPizza.IsEnabled = false;
                BestelBox.IsEnabled = false;
                
            }
            else
            {
                MessageBox.Show("niks geselecteerd");
            }

        }

        private void btnBestellenStatus_Click(object sender, RoutedEventArgs e)
        {
            if (bestelling.Status == true)
            {
                MessageBox.Show("Is besteld op : " + bestelling.Besteldatum);
            }
            else
            {
                MessageBox.Show("Is nog niet besteld, laatst gewijzigd op : " + bestelling.Besteldatum);

            }
        }

        double total = 0;

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }





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
