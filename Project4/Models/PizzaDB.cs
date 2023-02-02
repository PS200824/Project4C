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
                            Ingredient = new Ingredient()
                            {
                                IngredientId = (int)reader["ingredientId"],
                                Name = (string)reader["name"],
                            }
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

        /* SELECT i.ingredientId, i.name, i.price, i.pizzaID, u.name as PizzaName
           FROM pizzaingredienten i
           INNER JOIN pizzas u ON u.pizzaID = i.pizzaID
           WHERE i.ingredientId = @ingredientId
        */
    }
}
