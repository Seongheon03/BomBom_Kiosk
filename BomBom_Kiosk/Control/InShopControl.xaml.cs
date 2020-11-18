using BomBom_Kiosk.Service;
using BomBom_Kiosk.ViewModel;
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
    /// Interaction logic for InShopControl.xaml
    /// </summary>
    public partial class InShopControl : UserControl
    {
        public InShopControl()
        {
            InitializeComponent();
            Loaded += InShopControl_Loaded;
            IsVisibleChanged += InShopControl_IsVisibleChanged;
        }

        private void InShopControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue == true)
            {
                lvTables.UnselectAll();
                tbNext.IsEnabled = false;
            }
        }

        private void InShopControl_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = App.paymentViewModel;
        }

        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            App.uiManager.PopUC();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            App.uiManager.PushUC(UICategory.CHOOSEPAYMENT);
        }

        private void lvTables_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tbNext.IsEnabled = true;
        }
    }
}
