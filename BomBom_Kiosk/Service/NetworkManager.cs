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
        static NetworkStream networkStream = null;
        static TcpClient socketForServer = new TcpClient(App.serverHost, App.serverPort);
        public NetworkManager()
        {
            List<Menu> menuList = new List<Menu>();

            Login();
            SendCommonMsg(EMessageType.GROUP);
            SendCommonMsg(EMessageType.PERSONAL);
        }

        public static void Login()
        {
            JObject json = new JObject();
            json.Add("MSGType", 0);
            json.Add("id", "2203");
            json.Add("Content", "");
            json.Add("ShopName", "");
            json.Add("OrderNumber", "");
            json.Add("Group", "false");
            json.Add("Menus", "");
            
            string data = JsonConvert.SerializeObject(json);
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            networkStream = socketForServer.GetStream();
            networkStream.Write(bytes, 0, bytes.Length);
        }

        public static void SendCommonMsg(EMessageType type)
        {
            JObject json = new JObject();
            json.Add("MSGType", 1);
            json.Add("id", "2203");
            json.Add("Content", "집");
            json.Add("ShopName", "");
            json.Add("OrderNumber", "");
            if (type == EMessageType.GROUP)
            {
                json.Add("Group", "true");
            } else if (type == EMessageType.PERSONAL)
            {
                json.Add("Group", "false");
            }
            json.Add("Menus", "");
            
            string data = JsonConvert.SerializeObject(json);
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            networkStream = socketForServer.GetStream();
            networkStream.Write(bytes, 0, bytes.Length);
        }

        public static void SendOrderData()
        {
            OrderedDrink orderedDrink = new OrderedDrink();

            orderedDrink.Name = "뽀로로음료";
            orderedDrink.Count = 1;
            orderedDrink.Price = 1500;

            JObject menu = new JObject();
            menu.Add("Name", orderedDrink.Name);
            menu.Add("Count", orderedDrink.Count);
            menu.Add("Price", orderedDrink.Price);

            JArray menus = new JArray();
            menus.Add(menu);
            
            JObject json = new JObject();
           
            json.Add("MSGType", "2");
            json.Add("id", "2203");
            json.Add("Content", "");
            json.Add("ShopName", "봄봄");
            json.Add("OrderNumber", "002");
            json.Add("Menus", menus);

            string data = JsonConvert.SerializeObject(json);
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            networkStream = socketForServer.GetStream();
            networkStream.Write(bytes, 0, bytes.Length);
        }
    }

    class Menu
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
    }

    public enum EMessageType
    {
        PERSONAL,
        GROUP
    }
} 