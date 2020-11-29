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

        public List<MenuModel> GetDrinks()
        {
            cmd.CommandText = "SELECT * FROM menu";

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                List<MenuModel> menus = new List<MenuModel>();

                while (reader.Read())
                {
                    MenuModel menu = new MenuModel();
                    menu.Idx = int.Parse(reader["idx"].ToString());
                    menu.Name = reader["name"].ToString();
                    menu.OriginalPrice = int.Parse(reader["price"].ToString());
                    menu.DiscountPrice = int.Parse(reader["discount_price"].ToString());
                    menu.Image = reader["image"].ToString();
                    menu.Type = (ECategory)int.Parse(reader["type"].ToString());

                    menus.Add(menu);
                }

                 return menus;
            }
        }

        //private MenuModel GetDrink(int idx)
        //{
        //    cmd.CommandText = $"SELECT * FROM menu WHERE idx = {idx}";
        //    using (MySqlDataReader reader = cmd.ExecuteReader())
        //    {
        //        MenuModel menu = new MenuModel();

        //        while (reader.Read())
        //        {
        //            menu.Idx = int.Parse(reader["idx"].ToString());
        //            menu.Name = reader["name"].ToString();
        //            menu.Image = reader["image"].ToString();
        //            menu.OriginalPrice = int.Parse(reader["price"].ToString());
        //            menu.DiscountPrice = int.Parse(reader["discount_price"].ToString());
        //            menu.Type = (ECategory)int.Parse(reader["type"].ToString());
        //            menu.DiscountPrice = int.Parse(reader["discount_price"].ToString());
        //        }
        //        return menu;
        //    }
        //}

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
                    member.Barcode = reader["barcode"].ToString();
                    member.QRCode = reader["qr_code"].ToString();
                    member.Id = reader["id"].ToString();
                    member.Pw = reader["pw"].ToString();

                    members.Add(member);
                }
                
                return members;
            }
        }

        private MemberModel GetMember(int idx)
        {
            cmd.CommandText = $"SELECT * FROM member WHERE idx = {idx}";
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                MemberModel member = new MemberModel();

                while (reader.Read())
                {
                    member.Name = reader["name"].ToString();
                    member.Barcode = reader["barcode"].ToString();
                    member.QRCode = reader["qr_code"].ToString();
                    member.Idx = int.Parse(reader["idx"].ToString());
                    member.Id = reader["id"].ToString();
                    member.Pw = reader["pw"].ToString();
                }

                return member;
            }
        }

        public List<OrderedItem> GetOrderedItems()
        {
            List<OrderedItem> orderedItems = new List<OrderedItem>();

            cmd.CommandText = $"SELECT * FROM order_item";

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    OrderedItem orderedItem = new OrderedItem();
                    orderedItem.Idx = int.Parse(reader["idx"].ToString());
                    orderedItem.MenuName = reader["menu_name"].ToString();
                    orderedItem.MenuOriginalPrice = int.Parse(reader["menu_price"].ToString());
                    orderedItem.MenuDiscountPrice = int.Parse(reader["menu_discount_price"].ToString());
                    orderedItem.MenuType = (ECategory)int.Parse(reader["menu_type"].ToString());
                    orderedItem.Member.Idx = int.Parse(reader["member_idx"].ToString());
                    orderedItem.Count = int.Parse(reader["count"].ToString());
                    if (Int32.TryParse(reader["seat"].ToString(), out int seat))
                    {
                        orderedItem.Seat = seat;
                    }
                    orderedItem.OrderCode = reader["order_code"].ToString();
                    orderedItem.Place = (EOrderPlace)int.Parse(reader["place"].ToString());
                    orderedItem.Type = (EOrderType)int.Parse(reader["payment_type"].ToString());
                    orderedItem.OrderDateTime = DateTime.Parse(reader["order_date_time"].ToString());
                    orderedItems.Add(orderedItem);
                }
            }

            foreach (var item in orderedItems)
            {
                item.Member = GetMember(item.Member.Idx);
            }

            return orderedItems;
        }

        public void Payment()
        {
            foreach (var item in App.orderViewModel.OrderList)
            {
                AddOrderItem(item);
            }

            int index = GetLastIndex("order_number");
            string orderNumber = GetOrderNumber(index - 1);

            cmd.CommandText = "INSERT INTO order_number (idx, order_number) " +
                             $"VALUES ({index}, " +
                             $"{orderNumber})";

            cmd.ExecuteNonQuery();

            while (orderNumber.Length < 3)
            {
                orderNumber = "0" + orderNumber;
            }

            App.paymentViewModel.OrderNumber = orderNumber;
        }

        private void AddOrderItem(OrderedItem orderedItem)
        {
            OrderedItem orderInfo = App.paymentViewModel.OrderInfo;
            int index = GetLastIndex("order_item");

            if (orderInfo.Place == EOrderPlace.InShop)
            {
                cmd.CommandText = "INSERT INTO order_item (idx, menu_name, menu_price, menu_discount_price, menu_type, member_idx, count, seat, order_code, place, payment_type, order_date_time) " +
                                 $"VALUES ({index}," +
                                 $"'{orderedItem.MenuName}', " +
                                 $"{orderedItem.MenuOriginalPrice}, " +
                                 $"{orderedItem.MenuDiscountPrice}, " +
                                 $"{(int)orderedItem.MenuType}, " +
                                 $"{orderInfo.Member.Idx}, " +
                                 $"{orderedItem.Count}, " +
                                 $"{orderInfo.Seat}, " +
                                 $"'{orderInfo.OrderCode}', " +
                                 $"{(int)orderInfo.Place}, " +
                                 $"{(int)orderInfo.Type}, " +
                                 $"'{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}')";

                App.paymentViewModel.Tables.Where(x => x.Number == orderInfo.Seat).FirstOrDefault().IsUsing = true;
            }
            else
            {
                cmd.CommandText = "INSERT INTO order_item (idx, menu_name, menu_price, menu_discount_price, menu_type, member_idx, count, order_code, place, payment_type, order_date_time)" +
                                 $"VALUES ({index}," +
                                 $"'{orderedItem.MenuName}', " +
                                 $"{orderedItem.MenuOriginalPrice}, " +
                                 $"{orderedItem.MenuDiscountPrice}, " +
                                 $"{(int)orderedItem.MenuType}, " +
                                 $"{orderInfo.Member.Idx}, " +
                                 $"{orderedItem.Count}, " +
                                 $"'{orderInfo.OrderCode}', " +
                                 $"{(int)orderInfo.Place}, " +
                                 $"{(int)orderInfo.Type}, " +
                                 $"'{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}')";
            }

            cmd.ExecuteNonQuery();
        }

        private int GetLastIndex(string tableName)
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

        private string GetOrderNumber(int index)
        {
            int orderNumber = 1;

            cmd.CommandText = "SELECT order_number FROM order_number WHERE idx=" + index;

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    orderNumber = int.Parse(reader["order_number"].ToString()) + 1;
                }
            }

            if (orderNumber == 100)
            {
                orderNumber = 1;
            }

            return orderNumber.ToString();
        }

        public TimeSpan GetTime()
        {
            TimeSpan time = new TimeSpan();

            cmd.CommandText = "SELECT * FROM run_time";

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    time = TimeSpan.Parse(reader["run_time"].ToString());
                }
            }

            return time;
        }

        public void SaveTime(TimeSpan usedTime)
        {
            cmd.CommandText = "UPDATE run_time " +
                             $"SET run_time='{usedTime.ToString()}'";
            cmd.ExecuteNonQuery();
        }
    }
}
