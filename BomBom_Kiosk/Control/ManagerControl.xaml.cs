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
using LiveCharts;
using LiveCharts.Wpf;
using System.Windows.Threading;

namespace BomBom_Kiosk.Control
{
    /// <summary>
    /// ManagerControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ManagerControl : UserControl
    {
        public ManagerControl()
        {
            InitializeComponent();

            Loaded += ManagerControl_Loaded;
        }

        private void ManagerControl_Loaded(object sender, RoutedEventArgs e)
        {
            IsVisibleChanged += ManagerControl_IsVisibleChanged;
            DataContext = App.managerViewModel;
        }

        private void ManagerControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (((bool)e.NewValue) == true)
            {
                App.managerViewModel.OrderedItems = App.dbManager.GetOrderedItems();
            }
        }
    }
}
