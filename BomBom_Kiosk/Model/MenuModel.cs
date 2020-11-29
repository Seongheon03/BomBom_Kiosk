using Prism.Mvvm;

namespace BomBom_Kiosk.Model
{
    public class MenuModel : BindableBase
    {
        public int Idx { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public ECategory Type { get; set; }
        public int OriginalPrice { get; set; }

        private int _price;
        public int Price
        {
            get => _price;
            set => SetProperty(ref _price, value);
        }

        private int _discountPrice;
        public int DiscountPrice
        {
            get => _discountPrice;
            set
            {
                SetProperty(ref _discountPrice, value);
                Price = OriginalPrice - DiscountPrice;
            }
        }
    }
}
