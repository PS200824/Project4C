using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project4.Models
{
    public class Ingredient
    {
        private int ingredientId { get; set; }

        public int IngredientId
        {
            get { return ingredientId; }
            set { ingredientId = value; }
        }
        private string name { get; set; }
        public string Name 
        { 
            get { return name; }
            set { name = value; }
        }

    }
}
