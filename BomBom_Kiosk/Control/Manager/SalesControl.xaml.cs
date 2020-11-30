using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace BomBom_Kiosk.Control.Manager
{
    /// <summary>
    /// Interaction logic for SalesControl.xaml
    /// </summary>
    public partial class SalesControl : UserControl
    {
        public SalesControl()
        {
            InitializeComponent();

            SetNaviItems();

            IsVisibleChanged += SalesControl_IsVisibleChanged;
        }

        private void SalesControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue == true)
            {
                lvNavi.SelectedIndex = 0;
            }
        }

        private void lvNavi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SalesMenu selectedMenu = (SalesMenu)lvNavi.SelectedIndex;

            CollapseVisibility();

            switch (selectedMenu)
            {
                case SalesMenu.Total:
                    ctrlTotalSales.Visibility = Visibility.Visible;
                    break;
                case SalesMenu.Daily:
                    ctrlDailySales.Visibility = Visibility.Visible;
                    break;
                case SalesMenu.ByMember:
                    ctrlSalesByMember.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void CollapseVisibility()
        {
            ctrlTotalSales.Visibility = Visibility.Collapsed;
            ctrlDailySales.Visibility = Visibility.Collapsed;
            ctrlSalesByMember.Visibility = Visibility.Collapsed;
        }

        private void SetNaviItems()
        {
            lvNavi.Items.Add(new SalesNaviData(SalesMenu.Total, "총매출"));
            lvNavi.Items.Add(new SalesNaviData(SalesMenu.Total, "일별"));
            lvNavi.Items.Add(new SalesNaviData(SalesMenu.Total, "회원별"));
        }
    }

    public class SalesNaviData
    {
        public SalesMenu Idx { get; set; }
        public string Title { get; set; }

        public SalesNaviData(SalesMenu Idx, string Title)
        {
            this.Idx = Idx;
            this.Title = Title;
        }
    }

    public enum SalesMenu
    {
        Total,
        Daily,
        ByMember
    }
}
