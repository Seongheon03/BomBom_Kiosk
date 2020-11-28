using Prism.Mvvm;

namespace BomBom_Kiosk.Model
{
    public class MemberModel : BindableBase
    {
        public int Idx { get; set; }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
        public string Barcode { get; set; }
        public string QRCode { get; set; }
        public string Id { get; set; }
        public string Pw { get; set; }
    }
}
