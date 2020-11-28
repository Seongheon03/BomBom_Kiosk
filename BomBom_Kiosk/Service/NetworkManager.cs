using BomBom_Kiosk.Model;
using MySql.Data.MySqlClient.Memcached;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Crypto.Tls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BomBom_Kiosk.Service
{
    public class NetworkManager
    {
         NetworkStream networkStream = null;
         TcpClient socketForServer = new TcpClient(App.serverHost, App.serverPort);
        public NetworkManager()
        {
            Login();
            SendCommonMsg(EMessageType.GROUP, "aaa");
            SendCommonMsg(EMessageType.PERSONAL, "aaa");
            getMsg();
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
            
            sendData(json);
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
            
            sendData(json);
        }

        public void SendOrderData(List<OrderedDrink> orderedDrinks)
        {
            JObject menu = new JObject();
            JArray menus = new JArray();
            foreach (var orderedDrink in orderedDrinks)
            {
                menu.Add("Name", orderedDrink.Name);
                menu.Add("Count", orderedDrink.Count);
                menu.Add("Price", orderedDrink.Price);
                menus.Add(menu);
            }

            JObject json = new JObject();
           
            json.Add("MSGType", "2");
            json.Add("id", "2203");
            json.Add("Content", "");
            json.Add("ShopName", "봄봄");
            json.Add("OrderNumber", "002");
            json.Add("Menus", menus);

            sendData(json);
        }

        public void getMsg()
        {

            NetworkStream networkStream = socketForServer.GetStream();

            if (networkStream.CanRead)
            {
                byte[] bytes = new byte[1024];
                networkStream.Read (bytes, 0, (int)1024);
                string message = Encoding.UTF8.GetString (bytes);
                Console.WriteLine(message);
            }
        }

        public void sendData(JObject json)
        {
            string data = JsonConvert.SerializeObject(json);
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            networkStream = socketForServer.GetStream();
            networkStream.Write(bytes, 0, bytes.Length);
        }
    }

    public enum EMessageType
    {
        PERSONAL,
        GROUP
    }
} 