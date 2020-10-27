using BomBom_Kiosk.ViewModel;
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
            DataContext = App.paymentViewModel;
        }
    }
}
