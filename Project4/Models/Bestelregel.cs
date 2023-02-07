using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project4.Models
{
    public class Bestelregel
    {
        public int BestelregelId { get; set; }
        public Pizza Pizza { get; set; }

        public int BestellingId { get; set; }
        /*        public Bestelling Bestelling { get; set; }
        */

        public PizzaGrootte PizzaGrootte { get; set; }

        public int Aantal { get; set; }

        public decimal Regelprijs { get { return Aantal * (Pizza.Price + PizzaGrootte.Factor);  } }//c

    }


}
