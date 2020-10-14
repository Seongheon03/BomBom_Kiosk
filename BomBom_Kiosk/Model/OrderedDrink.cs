using Prism.Mvvm;

namespace BomBom_Kiosk.Model
{
    public class OrderedDrink : BindableBase
    {
        public int MenuIdx { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }

        private int _totalPrice;
        public int TotalPrice
        {
            get => _totalPrice;
            set => SetProperty(ref _totalPrice, value);
        }

        private int _count = 0;
        public int Count 
        {
            get => _count;
            set => SetProperty(ref _count, value); 
        }
    }
}
