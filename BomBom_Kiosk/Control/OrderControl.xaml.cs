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
        }

        private void lvDrinks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lvDrinks.UnselectAll();
        }

        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            App.uiManager.PopUC();
        }

        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {
            App.uiManager.PushUC(UICategory.CHOOSEPLACE);
        }
    }
}
