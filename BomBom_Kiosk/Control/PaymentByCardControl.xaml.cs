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
        public PaymentByCardControl()
        {
            InitializeComponent();
            Loaded += PaymentByCardControl_Loaded;
        }

        private void PaymentByCardControl_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = App.orderViewModel;
            IsVisibleChanged += PaymentByCardControl_IsVisibleChanged;
        }

        private void PaymentByCardControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue == true)
            {
                webcam.CameraIndex = 0;
                tbStatus.Visibility = Visibility.Hidden;
            }
            else
            {
                webcam.DisposeCamera();
            }
        }

        private void webcam_QrDecoded(object sender, string qrCode)
        {
            if (App.paymentViewModel.IsExistMember(qrCode))
            {
                tbStatus.Visibility = Visibility.Hidden;
                App.uiManager.PushUC(UICategory.PAYMENTRESULT);
            }
            else
            {
                tbStatus.Visibility = Visibility.Visible;
            }
        }

        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            App.uiManager.PopUC();
        }
    }
}
