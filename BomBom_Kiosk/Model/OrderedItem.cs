using Prism.Mvvm;
using System;

namespace BomBom_Kiosk.Model
{
    public class OrderedItem : BindableBase
    {
        public int Idx { get; set; }
        public MenuModel Menu { get; set; } = new MenuModel();
        public MemberModel Member { get; set; } = new MemberModel();

        private int _count = 0;
        public int Count
        {
            get => _count;
            set => SetProperty(ref _count, value);
        }

        public int? Seat { get; set; }

        private string _orderCode;
        public string OrderCode
        {
            get => _orderCode;
            set => SetProperty(ref _orderCode, value);
        }

        public EOrderPlace Place { get; set; }
        public EOrderType Type { get; set; }
        public DateTime OrderDateTime { get; set; }

        private int _totalPrice;
        public int TotalPrice
        {
            get => _totalPrice;
            set => SetProperty(ref _totalPrice, value);
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
