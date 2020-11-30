using BomBom_Kiosk.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;

namespace BomBom_Kiosk.Control.Manager.Sales
{
    /// <summary>
    /// Interaction logic for DailySalesControl.xaml
    /// </summary>
    public partial class DailySalesControl : UserControl, INotifyPropertyChanged
    {
        private readonly int HOUR_INTERVAL = 2;

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private DateTime _selectedDate = DateTime.Now;
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set
            {
                _selectedDate = value;
                NotifyPropertyChanged(nameof(SelectedDate));
            }
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

        public DailySalesControl()
        {
            InitializeComponent();

            DataContext = this;

            IsVisibleChanged += DailySalesControl_IsVisibleChanged;
        }

        private void DailySalesControl_IsVisibleChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue == true)
            {
                InitDatas();
            }
        }

        private void InitDatas()
        {
            TotalSales = 0;
            SetSalesListView();
        }

        private void SetSalesListView()
        {
            lvSales.Items.Clear();

            int oldValue = 0;

            List<OrderedItem> orderedItems = App.managerViewModel.OrderedItems;

            for (int i = 2; i <= 24; i += HOUR_INTERVAL)
            {
                int totalPrice = 0;

                foreach (var orderedItem in orderedItems)
                {
                    if (orderedItem.OrderDateTime.Date == SelectedDate.Date && oldValue <= orderedItem.OrderDateTime.Hour && (oldValue + HOUR_INTERVAL) >= orderedItem.OrderDateTime.Hour)
                    {
                        totalPrice += orderedItem.MenuPrice;
                    }
                }

                lvSales.Items.Add(new Model.Sales()
                {
                    TotalPrice = totalPrice,
                    Time = $"{GetHour(oldValue)}시 ~ {GetHour(i)}시"
                });

                TotalSales += totalPrice;
                oldValue = i;
            }
        }

        private string GetHour(int hour)
        {
            return hour.ToString().Length == 1 ? "0" + hour : hour.ToString();
        }

        private void btnPrev_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SelectedDate = SelectedDate.AddDays(-1);
            InitDatas();
        }

        private void btnNext_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SelectedDate = SelectedDate.AddDays(1);
            InitDatas();
        }
    }
}
