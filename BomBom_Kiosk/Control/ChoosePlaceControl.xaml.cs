using BomBom_Kiosk.Service;
using System.Windows;
using System.Windows.Controls;

namespace BomBom_Kiosk.Control
{
    /// <summary>
    /// Interaction logic for ChoosePlaceControl.xaml
    /// </summary>
    public partial class ChoosePlaceControl : UserControl
    {
        public ChoosePlaceControl()
        {
            InitializeComponent();
        }

        private void goBack_Click(object sender, RoutedEventArgs e)
        {
            App.uiManager.PopUC();
        }

        private void InShop_Click(object sender, RoutedEventArgs e)
        {
            App.paymentViewModel.OrderInfo.Place = Model.EOrderPlace.InShop;
            App.uiManager.PushUC(UICategory.INSHOP);
        }

        private void TakeOut_Click(object sender, RoutedEventArgs e)
        {
            App.paymentViewModel.OrderInfo.Place = Model.EOrderPlace.Packing;
            App.uiManager.PushUC(UICategory.CHOOSEPAYMENT);
        }
    }
}
