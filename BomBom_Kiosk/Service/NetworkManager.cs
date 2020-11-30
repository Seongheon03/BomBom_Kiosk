using BomBom_Kiosk.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows;

namespace BomBom_Kiosk.Service
{
    public class NetworkManager
    {
        TcpClient client = null;

        private void setClient() {
            try
            {
                client = new TcpClient(App.serverHost, App.serverPort);
            } catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public NetworkManager()
        {
            Thread connect = new Thread(() => ConnectServer());
            connect.Start();
            Thread getMsg = new Thread(() => GetMsg());
            getMsg.Start();
            
        }

        public void ConnectServer()
        {
            var client = new TcpClient();
            var result = client.BeginConnect(App.serverHost, App.serverPort, null, null);
            var success = result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(1));
            if (!success)
            {
                MessageBox.Show("서버 연결 실패");
                Login();
            } 
            else
            {
                MessageBox.Show("서버 연결 성공");
                Login();
            }
        }

        public void Login()
        {
            JObject json = new JObject();
            json.Add("MSGType", 0);
            json.Add("id", "2203");
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
            json.Add("id", "2203");
            json.Add("Content", content);
            json.Add("ShopName", "봄봄");
            json.Add("OrderNumber", "");
            if (type == EMessageType.GROUP)
            {
                json.Add("Group", "true");
            } else if (type == EMessageType.PERSONAL)
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
            json.Add("id", "2203");
            json.Add("Content", content);
            json.Add("ShopName", "봄봄");
            json.Add("OrderNumber", "");
            json.Add("Group", "true");
            json.Add("Menus", "");
            
            SendData(json);
        }

        public void SendOrderData(List<OrderedItem> orderedDrinks)
        {
            JObject menu = new JObject();
            JArray menus = new JArray();
            foreach (var orderedDrink in orderedDrinks)
            {
                menu.Add("Name", orderedDrink.MenuName);
                menu.Add("Count", orderedDrink.Count);
                menu.Add("Price", orderedDrink.MenuPrice);
                menus.Add(menu);
            }

            JObject json = new JObject();
           
            json.Add("MSGType", "2");
            json.Add("id", "2203");
            json.Add("Content", "");
            json.Add("ShopName", "봄봄");
            json.Add("OrderNumber", "002");
            if (orderedDrinks.Count > 1)
            {
                json.Add("Group", "true");
            }
            else
            {
                json.Add("Group", "false");
            }
            json.Add("Menus", menus);

            SendData(json);
        }

        public void GetMsg()
        {
            try
            {
                setClient();
                NetworkStream networkStream = client.GetStream();

                while (networkStream.CanRead)
                {
                    IAsyncResult asyncResult = client.BeginConnect(App.serverHost, App.serverPort, null, null);
                    if (asyncResult.AsyncWaitHandle.WaitOne(0, false))
                    {
                        byte[] bytes = new byte[1024];
                        networkStream.Read (bytes, 0, (int)1024);
                        string message = Encoding.UTF8.GetString (bytes);
                        if (message.Contains("총매출액"))
                        {
                           SendTotal();
                        }
                        else
                        {
                           MessageBox.Show(message);
                        }
                    }
                }

            }
            catch
            {
            }
        }

        public void SendData(JObject json)
        {
            string data = JsonConvert.SerializeObject(json);
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            
            try
            {
                setClient();
                NetworkStream networkStream = client.GetStream();
                networkStream.Write(bytes, 0, bytes.Length);
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