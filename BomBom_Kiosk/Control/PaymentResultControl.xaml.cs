using BomBom_Kiosk.ViewModel;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BomBom_Kiosk.Control
{
    /// <summary>
    /// Interaction logic for PaymentResultControl.xaml
    /// </summary>
    public partial class PaymentResultControl : UserControl
    {
        public OrderViewModel OrderViewModel { get; set; } = App.orderViewModel;
        public PaymentViewModel PaymentViewModel { get; set; } = App.paymentViewModel;

        public PaymentResultControl()
        {
            InitializeComponent();
            Loaded += PaymentResultControl_Loaded;
        }

        private void PaymentResultControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            DataContext = this;
            IsVisibleChanged += PaymentResultControl_IsVisibleChanged;
        }

        private async void PaymentResultControl_IsVisibleChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue == true)
            {
                tbOrderNumber.Text = "주문 번호 : " + App.dbManager.Payment();

                await Task.Run(() => Thread.Sleep(TimeSpan.FromSeconds(5)));
                App.orderViewModel.ResetData();
                App.uiManager.PushUC(Service.UICategory.HOME);
            }
        }
    }
}
