using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BomBom_Kiosk.Model
{
    public class MenuModel
    {
        public int Idx { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public ECategory Category { get; set; }
        public int Price { get; set; }
        public int DiscountPrice { get; set; }
    }
}
