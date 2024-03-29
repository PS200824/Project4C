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
    /// Interaction logic for MenuPage.xaml
    /// </summary>
    public partial class MenuPage : Window
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
        private ObservableCollection<Pizza> pizza = new ObservableCollection<Pizza>();
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

        private ObservableCollection<Ingredient> ingredients = new ObservableCollection<Ingredient>();
        public ObservableCollection<Ingredient> Ingredients
        {
            get { return ingredients; }
            set { ingredients = value; OnPropertyChanged(); }
        }
        private Pizza? selectedIngredient;
        public Pizza? SelectedIngredient
        {
            get { return selectedIngredient; }
            set { selectedIngredient = value; OnPropertyChanged(); }
        }
        #endregion

        public MenuPage()
        {
            InitializeComponent();
            PopulateMenus();
            DataContext = this;
        }

        private void PopulateMenus()
        {
            string dbResult = db.GetPizza(Pizza);
            if (dbResult != PizzaDB.OK)
            {
                MessageBox.Show(dbResult + serviceDeskBericht);
            }
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
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
