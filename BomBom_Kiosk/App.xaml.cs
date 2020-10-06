using BomBom_Kiosk.ViewModel;
using System.Windows;

namespace BomBom_Kiosk
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static readonly string connStr = "Server=10.80.161.127;Database=kiosk;Uid=maryoh;Pwd=1234;";

        public static OrderViewModel orderViewModel = new OrderViewModel();
    }
}
