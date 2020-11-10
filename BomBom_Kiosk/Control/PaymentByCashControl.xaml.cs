using BomBom_Kiosk.Service;
using BomBom_Kiosk.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace BomBom_Kiosk.Control
{
    /// <summary>
    /// Interaction logic for PaymentByCashControl.xaml
    /// </summary>
    public partial class PaymentByCashControl : UserControl
    {
        public OrderViewModel OrderViewModel { get; set; } = App.orderViewModel;
        public PaymentViewModel PaymentViewModel { get; set; } = App.paymentViewModel;

        public PaymentByCashControl()
        {
            InitializeComponent();
            Loaded += PaymentByCashControl_Loaded;
        }

        private void PaymentByCashControl_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = this;
            Window.GetWindow(this).KeyDown += PaymentByCashControl_KeyDown;
            IsVisibleChanged += PaymentByCashControl_IsVisibleChanged;
        }

        private void PaymentByCashControl_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            tbBarcode.Focus();
        }

        private void PaymentByCashControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue == true)
            {
                tbBarcode.Text = "";
                tbStatus.Visibility = Visibility.Hidden;
            }
        }

        private void UserControl_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Return)
            {
                string name = App.dbManager.GetMember(tbBarcode.Text);

                if (name == null)
                {
                    tbBarcode.Text = "";
                    tbStatus.Visibility = Visibility.Visible;
                }
                else
                {
                    App.paymentViewModel.OrderInfo.Name = name;
                    tbStatus.Visibility = Visibility.Hidden;
                    App.uiManager.PushUC(UICategory.PAYMENTRESULT);
                }
            }
        }

        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            App.uiManager.PopUC();
        }
    }
}
