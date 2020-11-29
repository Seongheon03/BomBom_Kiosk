﻿using BomBom_Kiosk.Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BomBom_Kiosk.Control.Manager.Statistics
{
    /// <summary>
    /// Interaction logic for TotalSalesStatisticsControl.xaml
    /// </summary>
    public partial class TotalSalesStatisticsControl : UserControl
    {
        public Sales TotalSales { get; set; }
        public Sales CashSales { get; set; }
        public Sales CardlSales { get; set; }

        public TotalSalesStatisticsControl()
        {
            InitializeComponent();

            Loaded += TotalSalesStatisticsControl_Loaded;
        }

        private void TotalSalesStatisticsControl_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = this;
            IsVisibleChanged += TotalSalesStatisticsControl_IsVisibleChanged;
        }

        private void TotalSalesStatisticsControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (((bool)e.NewValue) == true)
            {
                TotalSales = GetSales(App.managerViewModel.OrderedItems);
                CardlSales = GetSales(App.managerViewModel.OrderedItems.Where(x => x.Type == EOrderType.Card).ToList());
                CashSales = GetSales(App.managerViewModel.OrderedItems.Where(x => x.Type == EOrderType.Cash).ToList());
            }
        }

        private Sales GetSales(List<OrderedItem> orderedItems)
        {
            Sales sales = new Sales();

            foreach (var item in orderedItems)
            {
                sales.TotalPrice += item.MenuOriginalPrice;
                sales.NetSales += item.MenuOriginalPrice - item.MenuDiscountPrice;
                sales.DiscountPrice += item.MenuDiscountPrice;
            }

            return sales;
        }
    }

    public class Sales
    {
        public int TotalPrice { get; set; }
        public int NetSales { get; set; }
        public int DiscountPrice { get; set; }
    }
}
