using BomBom_Kiosk.Model;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;

namespace BomBom_Kiosk.Control.Manager.Sales
{
    /// <summary>
    /// Interaction logic for SalesByMemberControl.xaml
    /// </summary>
    public partial class SalesByMemberControl : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private int _totalSales = 0;
        public int TotalSales
        {
            get => _totalSales;
            set
            {
                _totalSales = value;
                NotifyPropertyChanged(nameof(TotalSales));
            }
        }

        public SalesByMemberControl()
        {
            InitializeComponent();

            Loaded += SalesByMemberControl_Loaded;

            DataContext = this;
        }

        private void SalesByMemberControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            cbMember.SelectedIndex = 0;
            SetMemberComboBox();
        }

        private void SetMemberComboBox()
        {
            cbMember.Items.Clear();

            foreach (var member in App.paymentViewModel.Members)
            {
                cbMember.Items.Add(member.Name);
            }
        }

        private void cbMember_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TotalSales = 0;
            SetMenuDataGrid();
        }

        private void SetMenuDataGrid()
        {
            dgMenu.Items.Clear();

            List<OrderedItem> orderedItems = new List<OrderedItem>();

            foreach (var orderedItem in App.managerViewModel.OrderedItems)
            {
                if (orderedItem.Member.Name == cbMember.SelectedItem.ToString())
                {
                    OrderedItem item = orderedItems.Where(x => x.MenuName == orderedItem.MenuName).FirstOrDefault();

                    if (item == null)
                    {
                        orderedItems.Add(new OrderedItem()
                        {
                            MenuName = orderedItem.MenuName,
                            Count = orderedItem.Count,
                            TotalPrice = orderedItem.MenuPrice
                        });
                    }
                    else
                    {
                        item.Count += orderedItem.Count;
                        item.TotalPrice += orderedItem.Count * orderedItem.MenuPrice;
                    }
                }
            }

            foreach (var orderedItem in orderedItems)
            {
                dgMenu.Items.Add(orderedItem);
                TotalSales += orderedItem.TotalPrice;
            }
        }
    }
}
