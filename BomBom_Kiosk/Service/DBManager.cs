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

            MySqlCommand cmd = ConnectDB();

            if (cmd != null)
            {
                string query = "SELECT * FROM menu";
                cmd.CommandText = query;

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
            }

            return drinks;
        }

        public string GetMember(string barcode)
        {
            MySqlCommand cmd = ConnectDB();

            if (cmd != null)
            {
                string query = "SELECT * FROM member";
                cmd.CommandText = query;

                MySqlDataReader reader = cmd.ExecuteReader();

                while(reader.Read())
                {
                    if (reader["barcode"].ToString() == barcode)
                    {
                        return  reader["name"].ToString();
                    }
                }
            }

            return null;
        }

        private MySqlCommand ConnectDB()
        {
            MySqlConnection conn = new MySqlConnection(App.connStr);

            try
            {
                conn.Open();
                return conn.CreateCommand();
            }
            catch
            {
                MessageBox.Show("DB 연결이 되지 않았습니다.");
                return null;
            }
        }
    }
}
