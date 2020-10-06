using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BomBom_Kiosk.Model
{
    public class Category
    {
        public ECategory Type { get; set; }
        public string Name { get; set; }
    }

    public enum ECategory
    {
        COFFEE,
        SMOOTHIE,
        ADE
    }
}
