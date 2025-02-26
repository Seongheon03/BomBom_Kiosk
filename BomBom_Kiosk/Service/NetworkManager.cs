﻿using BomBom_Kiosk.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace BomBom_Kiosk.Service
{
    public class NetworkManager
    {
        NetworkStream networkStream = null;
        TcpClient client = null;

        private void SetClient() {
            try
            {
                client = new TcpClient(App.serverHost, App.serverPort);
                networkStream = client.GetStream();
            } 
            catch
            {
            }
        }

        public bool ConnectServer()
        {
            var client = new TcpClient();
            var result = client.BeginConnect(App.serverHost, App.serverPort, null, null);
            var success = result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(3));
            if (!success)
            {
                return false;
            } 
            else
            {
                Login();
                Thread thread = new Thread(() => GetMsg());
                thread.Start();
                return true;
            }
        }

        public void Login()
        {
            JObject json = new JObject();
            json.Add("MSGType", 0);
            json.Add("id", "2207");
            json.Add("Content", "");
            json.Add("ShopName", "");
            json.Add("OrderNumber", "");
            json.Add("Group", "false");
            json.Add("Menus", "");
            
            SendData(json);
        }

        public void SendCommonMsg(EMessageType type, string content)
        {
            JObject json = new JObject();
            json.Add("MSGType", 1);
            json.Add("id", "2207");
            json.Add("Content", content);
            json.Add("ShopName", "봄봄");
            json.Add("OrderNumber", "");
            if (type == EMessageType.GROUP)
            {
                json.Add("Group", "true");
            } 
            else if (type == EMessageType.PERSONAL)
            {
                json.Add("Group", "false");
            }
            json.Add("Menus", "");
            
            SendData(json);
        }

        private int GetTotalPrice(List<OrderedItem> orderedItems)
        {
            int totalPrice = 0;

            foreach (var item in orderedItems)
            {
                totalPrice += item.MenuOriginalPrice;
            }

            return totalPrice;
        }

        public void SendTotal()
        {
            int total = GetTotalPrice(App.managerViewModel.OrderedItems);
            string content = "총매출액: " + total;
            JObject json = new JObject();
            json.Add("MSGType", 1);
            json.Add("id", "2207");
            json.Add("Content", content);
            json.Add("ShopName", "봄봄");
            json.Add("OrderNumber", "");
            json.Add("Group", "false");
            json.Add("Menus", "");
            
            SendData(json);
        }

        public void SendOrderData(List<OrderedItem> orderedDrinks)
        {
            JArray menus = new JArray();
            foreach (var orderedDrink in orderedDrinks)
            {
                JObject menu = new JObject();
                menu.Add("Name", orderedDrink.MenuName);
                menu.Add("Count", orderedDrink.Count);
                menu.Add("Price", orderedDrink.MenuPrice);
                menus.Add(menu);
            }

            JObject json = new JObject();
           
            json.Add("MSGType", "2");
            json.Add("id", "2207");
            json.Add("Content", "");
            json.Add("ShopName", "봄봄");
            json.Add("OrderNumber", App.paymentViewModel.OrderNumber);
            json.Add("Group", "true");
            json.Add("Menus", menus);

            SendData(json);
        }

        public void GetMsg()
        {
            bool success = true ;
            while (success)
            {
                byte[] bytes = new byte[1024];
                networkStream.Read (bytes, 0, (int)1024);
                string message = Encoding.UTF8.GetString (bytes);
                if (message.Contains("총매출액"))
                {
                    SendTotal();
                }
                else if (!message.Contains("200"))
                {
                    MessageBox.Show(message);
                }

                var client = new TcpClient();
                var result = client.BeginConnect(App.serverHost, App.serverPort, null, null);
                success = result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(3));
            }
            MessageBox.Show("서버에 연결이 끊어졌습니다.");
        }

        public void SendData(JObject json)
        {
            string data = JsonConvert.SerializeObject(json);
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            
            try
            {
                SetClient();
                if (networkStream != null)
                {
                    networkStream.Write(bytes, 0, bytes.Length);
                }
            }
            catch
            {
                MessageBox.Show("서버 연결 실패");
            }
        }
    }

    public enum EMessageType
    {
        PERSONAL,
        GROUP
    }
} 