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
        public static readonly string connStr = "server=localhost;database=kiosk;uid=root;pwd=1234";
        public static readonly string serverHost = "10.80.161.175";
        public static readonly int serverPort = 80;
        public static NetworkManager network = new NetworkManager();

        public static DBManager dbManager = new DBManager();
        public static UIManager uiManager = new UIManager();
        public static OrderViewModel orderViewModel = new OrderViewModel();
        public static PaymentViewModel paymentViewModel = new PaymentViewModel();
        public static ManagerViewModel managerViewModel = new ManagerViewModel();
    }
}
