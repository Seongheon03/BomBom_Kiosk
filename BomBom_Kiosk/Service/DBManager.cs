﻿using BomBom_Kiosk.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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
