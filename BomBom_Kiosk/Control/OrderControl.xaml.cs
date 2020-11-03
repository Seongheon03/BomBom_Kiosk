using BomBom_Kiosk.Service;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;

namespace BomBom_Kiosk.Control
{
    /// <summary>
    /// Interaction logic for OrderControl.xaml
    /// </summary>
    public partial class OrderControl : UserControl
    {
        public OrderControl()
        {
            InitializeComponent();
            Loaded += OrderControl_Loaded;
        }

        private void OrderControl_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = App.orderViewModel;
            ((INotifyCollectionChanged)lvOrderList.Items).CollectionChanged += OrderControl_CollectionChanged;
        }

        private void OrderControl_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (lvOrderList.Items.Count == 0)
            {
                btnRemoveAll.IsEnabled = false;
                btnOrder.IsEnabled = false;
            }
            else
            {
                btnRemoveAll.IsEnabled = true;
                btnOrder.IsEnabled = true;
            }
        }

        private void lvDrinks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lvDrinks.UnselectAll();
        }

        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            if (App.orderViewModel.OrderList.Count != 0)
            {
                if (MessageBox.Show("주문을 취소하시겠습니까?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    App.orderViewModel.OrderList.Clear();
                }
                else
                {
                    return;
                }
            }

            App.uiManager.PopUC();
        }

        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {
            App.uiManager.PushUC(UICategory.CHOOSEPLACE);
        }
    }
}
