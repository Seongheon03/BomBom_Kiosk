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
}
