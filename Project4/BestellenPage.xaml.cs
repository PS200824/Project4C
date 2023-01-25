﻿using Project4.Models;
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
        private ObservableCollection<Pizzas> pizzas = new ObservableCollection<Pizzas>();
        public ObservableCollection<Pizzas> Pizzas
        {
            get { return pizzas; }
            set { pizzas = value; OnPropertyChanged(); }
        }
        private Pizzas? selectedPizza;
        public Pizzas? SelectedPizza
        {
            get { return selectedPizza; }
            set { selectedPizza = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Pizzas> bestellingen = new ();

        public ObservableCollection<Pizzas> Bestellingen
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
                Pizzas.Remove(SelectedPizza);

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

        }
    }
}
