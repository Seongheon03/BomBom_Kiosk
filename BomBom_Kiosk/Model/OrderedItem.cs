using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BomBom_Kiosk.Model
{
    public class OrderedItem
    {
        public int Idx { get; set; }
        public MenuModel Menu { get; set; } = new MenuModel();
        public MemberModel Member { get; set; } = new MemberModel();
        public int Count { get; set; }
        public int? Seat { get; set; }
        public string OrderCode { get; set; }
        public EOrderPlace Place { get; set; }
        public EOrderType Type { get; set; }
        public DateTime OrderDateTime { get; set; }
    }
    public class MenuModel
    {
        public int Idx { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int Price { get; set; }
        public int Type { get; set; }
        public int DiscountPrice { get; set; }
    }
    public class MemberModel
    {
        public int Idx { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; }
        public string QRCode { get; set; }
        public string Id { get; set; }
        public string Pw { get; set; }
    }
}
