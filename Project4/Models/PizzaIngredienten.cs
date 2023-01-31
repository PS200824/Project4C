using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project4.Models
{
    public class PizzaIngredienten
    {
        public int IngredientenId { get; set; }
        private string name { get; set; }
        public string Name 
        {
            get { return name; }
            set { name = value; }
        }
        public string PizzaId { get; set; }
        public Ingredient Ingredient { get; set; }
        public Pizzas Pizzas { get; set; }

    }
}
