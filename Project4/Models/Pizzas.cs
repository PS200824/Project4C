using System;
using System.Collections.Generic;
using System.Text;

namespace Project4.Models
{
    public class Pizzas
    {
        private int pizzaID;
        public int PizzaID
        {
            get { return pizzaID; }
            set { pizzaID = value; }
        }
        private string name = null!;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string? description;
        public string? Description
        {
            get { return description; }
            set { description = value; }
        }
        private decimal price;
        public decimal Price
        {
            get { return price; }
            set { price = value; }
        }

    }
}
