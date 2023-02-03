using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project4.Models
{
    public class Bestelling
    {
        public int BestellinglId { get; set; }
        public DateTime Besteldatum { get; set; }
        public bool Status { get; set; }
    }
}
