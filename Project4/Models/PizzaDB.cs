using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Text;

namespace Project4.Models
{
    public class PizzaDB
    {
        public static readonly string UNKNOWN = "Unknown";
        public static readonly string OK = "Ok";
        public static readonly string NOTFOUND = "NOTFOUND";

        private string connString = ConfigurationManager.ConnectionStrings["PizzaConn"].ConnectionString;

        public string GetPizza(ICollection<Pizza> pizzas)
        {

            if (pizzas == null)
            {
                throw new ArgumentException("Ongeldig argument bij gebruik van GetMeals");
            }

            string methodResult = "unknown";
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand sql = conn.CreateCommand();
                    sql.CommandText = @" SELECT m.PizzaID, m.PizzaName, m.description, m.price FROM pizza m ";
                    MySqlDataReader reader = sql.ExecuteReader();
                    while (reader.Read())
                    {
                        Pizza pizza = new ()
                        {
                            PizzaID = (int)reader["pizzaID"],
                            Name = (string)reader["PizzaName"],
                            Description = reader["description"] == DBNull.Value
                     ? null
                     : (string)reader["description"],
                            Price = (decimal)reader["price"],
                        };
                        pizzas.Add(pizza);
                    }
                    methodResult = "Ok";
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(nameof(GetPizza));
                    Console.Error.WriteLine(e.Message);
                    methodResult = e.Message;
                }
            }
            return methodResult;
        }

        public string UpdateStatus(int bestellingId, Bestelling bestelling)
        {
            if (bestelling == null || bestelling.Status == null || bestelling.Besteldatum == null)
            {
                throw new ArgumentException("Ongeldig argument bij gebruik van UpdateIngredient");
            }
            string methodResult = UNKNOWN;
            using (MySqlConnection conn = new(connString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand sql = conn.CreateCommand();
                    sql.CommandText = @" UPDATE bestelling SET bestelDatum=@bestelDatum, status=@status WHERE bestellingId = @bestellingId; ";
                    sql.Parameters.AddWithValue("@bestellingId", bestellingId);
                    sql.Parameters.AddWithValue("@status", bestelling.Status);
                    sql.Parameters.AddWithValue("@bestelDatum", bestelling.Besteldatum);
                    if (sql.ExecuteNonQuery() == 1)
                    {
                        methodResult = OK;
                    }
                    else
                    {
                        methodResult = $"Ingrediënt {bestelling.Status} kon niet gewijzigd worden.";
                    }
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(nameof(UpdateStatus));
                    Console.Error.WriteLine(e.Message);
                    methodResult = e.Message;
                }
            }
            return methodResult;
        }

        public string GetBestellingen(Bestelling bestellingen, out Bestelling? bestelling)
        {
            bestelling = null;

            if (bestellingen == null)
            {
                throw new ArgumentException("Ongeldig argument bij gebruik van GetMeals");
            }

            string methodResult = "unknown";
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand sql = conn.CreateCommand();
                    sql.CommandText = @" SELECT bestellingId ,bestelDatum, status FROM bestelling WHERE bestelDatum = @bestelDatum && status = @status";
                    sql.Parameters.AddWithValue("@bestelDatum", bestellingen.Besteldatum);
                    sql.Parameters.AddWithValue("@status", bestellingen.Status);

                    MySqlDataReader reader = sql.ExecuteReader();
                    while (reader.Read())
                    {
                        bestelling = new()
                        {
                            BestellinglId = (int)reader["bestellingId"],
                            Besteldatum = (DateTime)reader["bestelDatum"],
                            Status = (bool)reader["status"],
                        };
                        
                    }
                    methodResult = bestelling == null ? NOTFOUND : OK;
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(nameof(GetBestellingen));
                    Console.Error.WriteLine(e.Message);
                    methodResult = e.Message;
                }
            }
            return methodResult;
        }

        public string CreateBestellings(Bestelling bestellings)
        {

            if (bestellings == null)
            {
                throw new ArgumentException("Ongeldig argument bij gebruik van CreateIngredient");
            }

            string methodResult = "unknown";
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand sql = conn.CreateCommand();
                    sql.CommandText = @" INSERT INTO bestelling(bestelDatum, status) VALUES ( @bestelDatum, @status) ";
                    sql.Parameters.AddWithValue("@bestelDatum", bestellings.Besteldatum);
                    sql.Parameters.AddWithValue("@status", bestellings.Status);

                    if (sql.ExecuteNonQuery() == 1)
                    {
                        methodResult = OK;
                    }
                    else
                    {
                        methodResult = $"Ingrediënt {bestellings.BestellinglId} kon niet toegevoegd worden.";
                    }

                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(nameof(CreateBestellings));
                    Console.Error.WriteLine(e.Message);
                    methodResult = e.Message;
                }
            }
            return methodResult;
        }

        public string GetBestelregels(ICollection<Bestelregel> bestelregels, Bestelling bestelling)
        {
            if (bestelregels == null)
            {
                throw new ArgumentException("Ongeldig argument bij gebruik van GetMeals");
            }
            string methodResult = UNKNOWN;
            using (MySqlConnection conn = new(connString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand sql = conn.CreateCommand();
                    sql.CommandText = @"
                            SELECT  b.bestelregelId, b.bestellingId, b.pizzaGrootteId, b.Aantal, b.pizzaID, p.PizzaName, p.price, pg.description,  pg.factor
                            FROM bestelregel b 
                            INNER JOIN pizza p ON p.pizzaID = b.pizzaID 
                            INNER JOIN pizzagrootte pg ON pg.pizzaGrootteId = b.pizzaGrootteId
                            WHERE b.bestellingId = @bestellingId
                         ";
                    sql.Parameters.AddWithValue("@bestellingId", bestelling.BestellinglId);
                    MySqlDataReader reader = sql.ExecuteReader();
                    while (reader.Read())
                    {
                        Bestelregel bestelregel = new()
                        {
                            BestelregelId = (int)reader["bestelregelId"],
                            BestellingId = (int)reader["bestellingId"],
                            PizzaGrootte = new()
                            {
                                Id = (int)reader["pizzaGrootteId"],
                                Description = (string)reader["description"],
                                Factor = (decimal)reader["factor"]
                            },
                            Aantal = (int)reader["Aantal"],
                            Pizza = new(){
                               PizzaID = (int)reader["pizzaID"],
                               Name =  (string)reader["PizzaName"],
                               Price = (decimal)reader["price"]
                            },
                        };
                        bestelregels.Add(bestelregel);
                    }
                    methodResult = "Ok";
                    bestelling.Bestelregels = new List<Bestelregel>();
                    foreach (Bestelregel bestelregel in bestelregels)
                    {
                        bestelling.Bestelregels.Add(bestelregel);
                    }
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(nameof(GetBestelregels));
                    Console.Error.WriteLine(e.Message);
                    methodResult = e.Message;
                }
            }
            return methodResult;
        }

        public string CreateBestelRegels(Bestelregel bestelregels)
        {

            if (bestelregels == null || bestelregels.Aantal < 0 || bestelregels.PizzaGrootte.Id == 0 || bestelregels.BestellingId == 0 || bestelregels.Pizza.PizzaID == 0)
            {
                throw new ArgumentException("Ongeldig argument bij gebruik van CreateBestelRegels");
            }

            string methodResult = "unknown";
            using (MySqlConnection conn = new (connString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand sql = conn.CreateCommand();
                    sql.CommandText = @" INSERT INTO bestelregel (bestellingId, pizzaGrootteId, Aantal, pizzaID) 
                                        VALUES (@bestellingId, @pizzaGrootteId, @Aantal, @pizzaID) ";
                    sql.Parameters.AddWithValue("@bestellingId", bestelregels.BestellingId); 
                    sql.Parameters.AddWithValue("@pizzaGrootteId", bestelregels.PizzaGrootte.Id);
                    sql.Parameters.AddWithValue("@Aantal", bestelregels.Aantal);
                    sql.Parameters.AddWithValue("@pizzaID", bestelregels.Pizza.PizzaID);


                    if (sql.ExecuteNonQuery() == 1) 
                    {
                        methodResult = OK;
                    }
                    else
                    {
                        methodResult = $"Ingrediënt {bestelregels.BestelregelId} kon niet toegevoegd worden.";
                    }
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(nameof(CreateBestelRegels));
                    Console.Error.WriteLine(e.Message);
                    methodResult = e.Message;
                }
            }
            return methodResult;
        }

        public string DeleteBestelregel(int bestelregelId)
        {
            string methodResult = UNKNOWN;
            using (MySqlConnection conn = new(connString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand sql = conn.CreateCommand();
                    sql.CommandText = @" DELETE FROM bestelregel
                                         WHERE bestelregelId = @bestelregelId
                                         ";
                    sql.Parameters.AddWithValue("@bestelregelId", bestelregelId);
                    if (sql.ExecuteNonQuery() == 1)
                    {
                        methodResult = OK;
                    }
                    else
                    {
                        methodResult = $"Ingredient met id {bestelregelId} kon niet verwijderd worden.";
                    }
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(nameof(DeleteBestelregel));
                    Console.Error.WriteLine(e.Message);
                    methodResult = e.Message;
                }
            }
            return methodResult;
        }


        internal string GetPizzaGroottes(ICollection<PizzaGrootte> pizzaGroottes)
        {
            if (pizzaGroottes == null)
            {
                throw new ArgumentException("Ongeldig argument bij gebruik van GetMeals");
            }

            string methodResult = "unknown";
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand sql = conn.CreateCommand();
                    sql.CommandText = @" SELECT pizzaGrootteId, description, factor FROM pizzagrootte "; //select query to show in combobox 
                    MySqlDataReader reader = sql.ExecuteReader();
                    while (reader.Read())
                    {
                        PizzaGrootte pizzaGrootes = new() //this query 
                        {
                            Id = (int)reader["pizzaGrootteId"],
                            Factor = (decimal)reader["factor"],
                            Description = reader["description"] == DBNull.Value ? null : (string)reader["description"],
                        };
                        pizzaGroottes.Add(pizzaGrootes); //alles toevoegen
                    }
                    methodResult = "Ok";
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(nameof(GetPizzaGroottes));
                    Console.Error.WriteLine(e.Message);
                    methodResult = e.Message; //als de query niet wordt uitgevoerd krijg je dus hier die error
                }
            }
            return methodResult;
        }

        // call methode in db file to add (aantal, groote, pizzaid) in bestelregel table
        // get all pizzas from bestelregel table

    }
}
