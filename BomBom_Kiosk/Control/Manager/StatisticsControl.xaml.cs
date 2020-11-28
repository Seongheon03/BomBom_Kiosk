using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace BomBom_Kiosk.Control.Manager
{
    /// <summary>
    /// Interaction logic for StatisticsControl.xaml
    /// </summary>
    public partial class StatisticsControl : UserControl
    {
        public List<NaviData> NaviItems { get; set; } = new List<NaviData>();

        public StatisticsControl()
        {
            InitializeComponent();

            SetNaviItems();
            DataContext = this;
        }

        private void SetNaviItems()
        {
            NaviItems.Add(new NaviData(EMenu.TotalSalesStatistics, "총매출"));
            NaviItems.Add(new NaviData(EMenu.StatisticsByMenu, "메뉴별"));
            NaviItems.Add(new NaviData(EMenu.StatisticsByCategory, "카테고리별"));
            NaviItems.Add(new NaviData(EMenu.StatisticsBySeat, "좌석별"));
            NaviItems.Add(new NaviData(EMenu.DailyStatistics, "일별"));
            NaviItems.Add(new NaviData(EMenu.StatisticsByMember, "회원별"));
        }

        private void lvNavi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EMenu selectedMenu = (EMenu)lvNavi.SelectedIndex;
            
            CollapseVisibility();

            switch (selectedMenu)
            {
                case EMenu.TotalSalesStatistics:
                    ctrlTotalSalesStatistics.Visibility = Visibility.Visible;
                    break;
                case EMenu.StatisticsByMenu:
                    ctrlStatisticsByMenu.Visibility = Visibility.Visible;
                    break;
                case EMenu.StatisticsByCategory:
                    ctrlStatisticsByCategory.Visibility = Visibility.Visible;
                    break;
                case EMenu.StatisticsBySeat:
                    ctrlStatisticsBySeat.Visibility = Visibility.Visible;
                    break;
                case EMenu.DailyStatistics:
                    ctrlDailyStatistics.Visibility = Visibility.Visible;
                    break;
                case EMenu.StatisticsByMember:
                    ctrlStatisticsByMember.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void CollapseVisibility()
        {
            ctrlTotalSalesStatistics.Visibility = Visibility.Collapsed;
            ctrlStatisticsByMenu.Visibility = Visibility.Collapsed;
            ctrlStatisticsByCategory.Visibility = Visibility.Collapsed;
            ctrlStatisticsBySeat.Visibility = Visibility.Collapsed;
            ctrlDailyStatistics.Visibility = Visibility.Collapsed;
            ctrlStatisticsByMember.Visibility = Visibility.Collapsed;
        }
    }
    public class NaviData
    {
        public EMenu Idx { get; set; }
        public string Title { get; set; }

        public NaviData(EMenu Idx, string Title)
        {
            this.Idx = Idx;
            this.Title = Title;
        }
    }

    public enum EMenu
    {
        TotalSalesStatistics,
        StatisticsByMenu,
        StatisticsByCategory,
        StatisticsBySeat,
        DailyStatistics,
        StatisticsByMember
    }
}
