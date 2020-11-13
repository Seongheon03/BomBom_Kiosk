using BomBom_Kiosk.Service;
using BomBom_Kiosk.ViewModel;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Windows;

namespace BomBom_Kiosk
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static readonly string connStr = "Server=10.80.161.127;Database=kiosk;Uid=abc;Pwd=1234;";
        public static readonly string serverHost = "10.80.162.152";
        public static readonly int serverPort = 80;
        NetworkManager network = new NetworkManager();

        public static DBManager dbManager = new DBManager();
        public static UIManager uiManager = new UIManager();
        public static OrderViewModel orderViewModel = new OrderViewModel();
        public static PaymentViewModel paymentViewModel = new PaymentViewModel();

        public delegate void LoadingEventHandler(bool isLoading, string status);
        public static event LoadingEventHandler LoadingAction;

        public static async void InitData()
        {
            LoadingAction?.Invoke(true, "DB에 연결중입니다...");
            if (await Task.Run(() => dbManager.ConnectDB()))
            {
                LoadingAction?.Invoke(true, "메뉴를 불러오는 중입니다...");
                await Task.Run(() => orderViewModel.InitData()); 
                LoadingAction?.Invoke(true, "회원을 불러오는 중입니다...");

                LoadingAction?.Invoke(false, "환영합니다. 주문을 원하시면 아래 주문하기 버튼을 클릭해주세요.");
                return;
            }
            LoadingAction?.Invoke(true, "DB에 연결되지 않았습니다.");
        }
    }
}
