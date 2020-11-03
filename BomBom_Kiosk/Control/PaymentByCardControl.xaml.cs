using BomBom_Kiosk.Service;
using System.Windows;
using System.Windows.Controls;

namespace BomBom_Kiosk.Control
{
    /// <summary>
    /// Interaction logic for PaymentByCardControl.xaml
    /// </summary>
    public partial class PaymentByCardControl : UserControl
    {
        private DBManager dbManager = new DBManager();

        public PaymentByCardControl()
        {
            InitializeComponent();
            Loaded += PaymentByCardControl_Loaded;
        }

        private void PaymentByCardControl_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = App.orderViewModel;
            webcam.CameraIndex = 0;
            IsVisibleChanged += PaymentByCardControl_IsVisibleChanged;
        }

        private void PaymentByCardControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue == true)
            {
                tbStatus.Visibility = Visibility.Hidden;
            }
        }

        private void webcam_QrDecoded(object sender, string qrCode)
        {
            string name = dbManager.GetMember(qrCode);

            if (name == null)
            {
                tbStatus.Visibility = Visibility.Visible;
            }
            else
            {
                App.paymentViewModel.OrderInfo.Code = qrCode;
                App.paymentViewModel.OrderInfo.Name = name;
                tbStatus.Visibility = Visibility.Hidden;
                App.uiManager.PushUC(UICategory.PAYMENTRESULT);
            }
        }

        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            App.uiManager.PopUC();
        }
    }
}
