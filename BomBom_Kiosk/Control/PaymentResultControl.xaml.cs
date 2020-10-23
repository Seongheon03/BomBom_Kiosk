using System.Windows.Controls;

namespace BomBom_Kiosk.Control
{
    /// <summary>
    /// Interaction logic for PaymentResultControl.xaml
    /// </summary>
    public partial class PaymentResultControl : UserControl
    {
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
