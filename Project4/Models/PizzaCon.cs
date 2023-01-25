using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Project4.Models
{
    public class PizzaCon
    {
        private string connString =
            ConfigurationManager.ConnectionStrings["PizzaConn"].ConnectionString;

    }
}
