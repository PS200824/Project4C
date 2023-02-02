using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project4.Models
{
    public class PizzaGrootte
    {
        public int Id { get; set; }
        public string Description{ get; set; }
        public decimal Factor { get; set; }             // bijv 0.7 voor klein, 1.0 voor normaal, etc
    }
}
