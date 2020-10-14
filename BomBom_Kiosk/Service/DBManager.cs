using BomBom_Kiosk.Model;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Windows;

namespace BomBom_Kiosk.Service
{
    public class DBManager
    {
        public List<Drink> GetDrinks()
        {
            List<Drink> drinks = new List<Drink>();

            MySqlConnection conn = new MySqlConnection(App.connStr);
            MySqlCommand cmd = conn.CreateCommand();

            string query = "SELECT * FROM menu";
            cmd.CommandText = query;

            try
            {
                conn.Open();
            }
            catch
            {
                MessageBox.Show("DB 연결이 되지 않았습니다.");
                return drinks;
            }

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Drink drink = new Drink();
                drink.Idx = int.Parse(reader["idx"].ToString());
                drink.Name = reader["name"].ToString();
                drink.Price = int.Parse(reader["price"].ToString());
                drink.Image = reader["image"].ToString();
                drink.Category = (ECategory)int.Parse(reader["type"].ToString());

                drinks.Add(drink);
            }

            return drinks;
        }
    }
}
