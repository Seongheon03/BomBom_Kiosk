using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BomBom_Kiosk.Model
{
    public class MessagePacket
    {
        public EMsgType MessageType { get; set; }
        public string Id { get; set; }
        public string Content { get; set; }
        public string ShopName = "봄봄 구지점"; 
        public string Name { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
        public string OrderNumber { get; set; }
    }

    public enum EMsgType
    {
        LOGIN,
        MESSAGE,
        ORDERTYPE
    }
}
