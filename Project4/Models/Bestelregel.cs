using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project4.Models
{
    class Bestelregel
    {
        public int PizzaId { get; set; }
        public Pizzas Pizza { get; set; }

        public int BestellingId { get; set; }
        public Bestelling Bestelling { get; set; }


        public int PizzaGrootteId { get; set; }
        public PizzaGrootte PizzaGrootte { get; set; }

        public int Aantal { get; set; }

        public decimal Regelprijs { get { return Aantal * Pizza.Price * PizzaGrootte.factor;  } }

    }
}
