using BomBom_Kiosk.Service;
using BomBom_Kiosk.ViewModel;
using System.Windows;

namespace BomBom_Kiosk
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static readonly string connStr = "Server=10.80.161.127;Database=kiosk;Uid=abc;Pwd=1234;";

        public static UIManager uiManager = new UIManager();
        public static OrderViewModel orderViewModel = new OrderViewModel();
        public static PaymentViewModel paymentViewModel = new PaymentViewModel();
    }
}
