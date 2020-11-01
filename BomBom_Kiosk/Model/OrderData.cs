using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BomBom_Kiosk.Model
{
    public class OrderData : BindableBase
    {
        public EOrderPlace Place { get; set; }

        public EOrderType Type { get; set; }

        public int Table { get; set; }

        private string _code;
        public string Code
        {
            get => _code;
            set => SetProperty(ref _code, value);
        }

        private string _name;
        public string Name 
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
        
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
