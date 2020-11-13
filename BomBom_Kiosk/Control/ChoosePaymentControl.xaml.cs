using BomBom_Kiosk.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BomBom_Kiosk.Control
{
    /// <summary>
    /// Interaction logic for ChoosePaymentControl.xaml
    /// </summary>
    public partial class ChoosePaymentControl : UserControl
    {
        public ChoosePaymentControl()
        {
            InitializeComponent();
            DataContext = App.orderViewModel;
        }

        private void goBack_Click(object sender, RoutedEventArgs e)
        {
            App.uiManager.PopUC();
        }
        private void UseCash_Click(object sender, RoutedEventArgs e)
        {
            App.paymentViewModel.OrderInfo.Type = Model.EOrderType.Cash;
            App.uiManager.PushUC(UICategory.PAYMENTBYCASH);
        }

        private void UseCard_Click(object sender, RoutedEventArgs e)
        {
            App.paymentViewModel.OrderInfo.Type = Model.EOrderType.Card;
            App.uiManager.PushUC(UICategory.PAYMENTBYCARD);
        }
    }
}
