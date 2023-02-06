using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project4.Models
{
    public class Bestelling
    {
        public List<Bestelregel> Bestelregels {get; set; } 
        public int BestellinglId { get; set; }
        public DateTime Besteldatum { get; set; }
        public bool Status { get; set; }
        //public decimal Bestellingprijs()
        //{
            //decimal Resultaat = 0;
            //for (int i = 0; i < Bestelregels.length; i++)
            //{
                //Resultaat += Bestelregels[i].Regelprijs();
            //}
            //return Resultaat;
        //}
    }


}
