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

        public string GetPizza(ICollection<Pizzas> pizzas)
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
                        Pizzas pizza = new ()
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

            if (bestellings == null || bestellings.Besteldatum == null)
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

        public string GetBestelregels(ICollection<Bestelregel> bestelregels)
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
                         SELECT  b.bestelregelId, b.bestellingId, b.pizzaGrootteId, b.Aantal, b.pizzaID FROM bestelregel b
                         INNER JOIN pizza p ON p.pizzaID = i.pizzaID
                         WHERE b.bestelregelId = @bestelregelId;
                         ";
                    sql.Parameters.AddWithValue("@bestelregelId", bestelregels);
                    MySqlDataReader reader = sql.ExecuteReader();
                    while (reader.Read())
                    {
                        Bestelregel bestelregel = new()
                        {
                            BestelregelId = (int)reader["bestelregelId"],
                            BestellingId = (int)reader["bestellingId"],
                            PizzaGrootteId = (int)reader["pizzaGrootteId"],
                            Aantal = (int)reader["Aantal"],
                            PizzaID = (int)reader["pizzaID"],
                        };
                        bestelregels.Add(bestelregel);
                    }
                    methodResult = "Ok";
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

        public string GetIngredient(ICollection<Ingredient> ingredienten)
        {
            if (ingredienten == null)
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
                         SELECT i.ingredientId, i.name FROM ingredienten i
                         ";
                    MySqlDataReader reader = sql.ExecuteReader();
                    while (reader.Read())
                    {
                       Ingredient ingredients = new ()
                        {
                            IngredientId = (int)reader["ingredientId"],
                            Name = (string)reader["name"],
                        };
                        ingredienten.Add(ingredients);
                    }
                    methodResult = "Ok";
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(nameof(GetIngredient));
                    Console.Error.WriteLine(e.Message);
                    methodResult = e.Message;
                }
            }
            return methodResult;
        }
        public string GetPizzaIngredient(int ingredientId, out Ingredient? ingredient)
        {
            ingredient = null;
            string methodResult = UNKNOWN;
            using (MySqlConnection conn = new(connString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand sql = conn.CreateCommand();
                    sql.CommandText = @"
                         SELECT i.ingredientId, i.name, i.price, i.unitId, u.name as unitName
                         FROM ingredients i
                         INNER JOIN units u ON u.unitId = i.unitId
                         WHERE i.ingredientId = @ingredientId;
                         ";
                    sql.Parameters.AddWithValue("@ingredientId", ingredientId);
                    MySqlDataReader reader = sql.ExecuteReader();
                    while (reader.Read())
                    {
                        ingredient = new()
                        {
                            IngredientId = (int)reader["ingredientId"],
                            Name = (string)reader["name"],
                            /*PizzaIngredienten = new()
                            {
                                IngredientId = (int)reader["ingredientId"],
                                Name = (string)reader["name"],
                            }*/
                        };
                    }
                    methodResult = ingredient == null ? NOTFOUND : OK;
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(nameof(GetIngredient));
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
                    sql.CommandText = @" SELECT pizzaGrootteId, description, factor FROM pizzagrootte ";
                    MySqlDataReader reader = sql.ExecuteReader();
                    while (reader.Read())
                    {
                        PizzaGrootte pizzaGrootes = new()
                        {
                            Id = (int)reader["pizzaGrootteId"],
                            Factor = (decimal)reader["factor"],
                            Description = reader["description"] == DBNull.Value ? null : (string)reader["description"],
                        };
                        pizzaGroottes.Add(pizzaGrootes);
                    }
                    methodResult = "Ok";
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(nameof(GetPizzaGroottes));
                    Console.Error.WriteLine(e.Message);
                    methodResult = e.Message;
                }
            }
            return methodResult;
        }

        // call methode in db file to add (aantal, groote, pizzaid) in bestelregel table
        // get all pizzas from bestelregel table

    }
}
