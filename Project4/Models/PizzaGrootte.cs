using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project4.Models
{
    class PizzaGrootte
    {
        public string description{ get; set; }
        public decimal factor { get; set; }             // bijv 0.7 voor klein, 1.0 voor normaal, etc
    }
}
