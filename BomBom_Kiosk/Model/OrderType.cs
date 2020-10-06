using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BomBom_Kiosk.Model
{
    public class OrderType
    {
        public EOrderType Type { get; set; }
        public string Name { get; set; }
    }

    public enum EOrderType
    {
        InShop, 
        Packing
    }
}
