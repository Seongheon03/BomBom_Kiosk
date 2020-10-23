using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BomBom_Kiosk.Model
{
    public class OrderData
    {
        public EOrderPlace Place { get; set; }

        public EOrderType Type { get; set; }

        public int Table { get; set; }

        public string Barcode { get; set; }
        public string Name { get; set; }
        
    }

    public enum EOrderPlace
    {
        InShop,
        Packing
    }

    public enum EOrderType
    {
        Cash,
        Card
    }
}
