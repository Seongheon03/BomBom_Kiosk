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

namespace BomBom_Kiosk.Control.Payment
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
        }

        private void InShopControl_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = App.paymentViewModel;
        }
    }
}
