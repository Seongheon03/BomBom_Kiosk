using BomBom_Kiosk.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace BomBom_Kiosk.Service
{
    public class DBManager
    {
        private MySqlCommand cmd;

        public bool ConnectDB()
        {
            MySqlConnection conn = new MySqlConnection(App.connStr);

            try
            {
                conn.Open();
                cmd = conn.CreateCommand();

                return true;
            }
            catch
            {
                MessageBox.Show("DB 연결이 되지 않았습니다.");
                cmd =  null;

                return false;
            }
        }

        public List<Drink> GetDrinks()
        {
            cmd.CommandText = "SELECT * FROM menu";

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                List<Drink> drinks = new List<Drink>();

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

        public List<MemberModel> GetMembers()
        {
            cmd.CommandText = "SELECT * FROM member";

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                List<MemberModel> members = new List<MemberModel>();

                while (reader.Read())
                {
                    MemberModel member = new MemberModel();
                    member.Idx = int.Parse(reader["idx"].ToString());
                    member.Name = reader["name"].ToString();
                    member.Code = reader["code"].ToString();
                    member.Id = reader["id"].ToString();
                    member.Pw = reader["pw"].ToString();

                    members.Add(member);
                }
                
                return members;
            }
        }

        public string Payment()
        {
            foreach (var item in App.orderViewModel.OrderList)
            {
                AddOrderItem(item);
            }

            int index = GetIndex("order_number");

            cmd.CommandText = "INSERT INTO order_number (idx, member_idx) " +
                             $"VALUES ({index}, " +
                             $"{App.paymentViewModel.OrderInfo.MemberIdx})";

            cmd.ExecuteNonQuery();

            string orderNumber = index.ToString();

            while (orderNumber.Length < 3)
            {
                orderNumber = "0" + orderNumber;
            }

            return orderNumber;
        }

        private void AddOrderItem(OrderedDrink orderedDrink)
        {
            OrderData orderInfo = App.paymentViewModel.OrderInfo;
            int index = GetIndex("order_item");

            if (orderInfo.Place == EOrderPlace.InShop)
            {
                cmd.CommandText = "INSERT INTO order_item (idx, menu_idx, count, seat, order_code, place, payment_type, order_date_time) " +
                                 $"VALUES ({index}," +
                                 $"{orderedDrink.MenuIdx}, " +
                                 $"{orderedDrink.Count}, " +
                                 $"{orderInfo.Table}, " +
                                 $"'{orderInfo.Code}', " +
                                 $"{(int)orderInfo.Place}, " +
                                 $"{(int)orderInfo.Type}, " +
                                 $"'{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}')";

                App.paymentViewModel.Tables.Where(x => x.Number == orderInfo.Table).FirstOrDefault().IsUsing = true;
            }
            else
            {
                cmd.CommandText = "INSERT INTO order_item (idx, menu_idx, count, order_code, place, payment_type, order_date_time)" +
                                 $"VALUES ({index}," +
                                 $"{orderedDrink.MenuIdx}, " +
                                 $"{orderedDrink.Count}, " +
                                 $"{orderInfo.Table}, " +
                                 $"'{orderInfo.Code}', " +
                                 $"{(int)orderInfo.Place}, " +
                                 $"{(int)orderInfo.Type}, " +
                                 $"'{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}')";
            }

            cmd.ExecuteNonQuery();
        }

        private int GetIndex(string tableName)
        {
            int index = 0;

            cmd.CommandText = $"SELECT idx FROM {tableName}";

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    if (index < int.Parse(reader["idx"].ToString()))
                    {
                        index = int.Parse(reader["idx"].ToString());
                    }
                }

                return index + 1;
            }
        }

        public void SaveTime(TimeSpan usedTime)
        {
            if (cmd == null)
            {
                return;
            }

            DateTime totalTime = new DateTime();
            cmd.CommandText = "SELECT * FROM time";

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {

                while (reader.Read())
                {
                    totalTime = DateTime.Parse(reader["time"].ToString()) + usedTime;
                }
            }

            cmd.CommandText = "UPDATE time " +
                             $"SET time='{totalTime.ToString("yyyy-MM-dd HH:hh:ss")}'";
            cmd.ExecuteNonQuery();
        }
    }
}
