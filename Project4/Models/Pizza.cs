using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Project4.Models
{
    internal class Pizza
    {
        private string connString =
            ConfigurationManager.ConnectionStrings["PizzaConn"].ConnectionString;

    }
}
