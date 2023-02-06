using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project4.Models
{
    public class PizzaIngredienten
    {
        private int ingredientenId { get; set; }
        public int IngredientenId 
        { 
            get { return ingredientenId; }
            set { ingredientenId = value;}
        }
        private int pizzaId { get; set; }
        public int PizzaId
        {
            get { return pizzaId; }
            set { pizzaId = value; }
        }
        public Ingredient Ingredient { get; set; }
        public Pizza Pizzas { get; set; }

    }
}
