using BomBom_Kiosk.Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BomBom_Kiosk.Control.Manager.Sales
{
    /// <summary>
    /// Interaction logic for TotalSalesControl.xaml
    /// </summary>
    public partial class TotalSalesControl : UserControl
    {
        public TotalSalesControl()
        {
            InitializeComponent();

            Loaded += TotalSalesControl_Loaded;
        }

        private void TotalSalesControl_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = this;

            IsVisibleChanged += TotalSalesControl_IsVisibleChanged;
        }

        private void TotalSalesControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (((bool)e.NewValue) == true)
            {
                spTotal.DataContext = GetSales(App.managerViewModel.OrderedItems);
                spCard.DataContext = GetSales(App.managerViewModel.OrderedItems.Where(x => x.Type == EOrderType.Card).ToList());
                spCash.DataContext = GetSales(App.managerViewModel.OrderedItems.Where(x => x.Type == EOrderType.Cash).ToList());
            }
        }

        private Model.Sales GetSales(List<OrderedItem> orderedItems)
        {
            Model.Sales sales = new Model.Sales();

            foreach (var item in orderedItems)
            {
                sales.TotalPrice += item.MenuOriginalPrice;
                sales.NetSales += item.MenuOriginalPrice - item.MenuDiscountPrice;
                sales.DiscountPrice += item.MenuDiscountPrice;
            }

            return sales;
        }
    }
}
