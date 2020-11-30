using BomBom_Kiosk.Model;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BomBom_Kiosk.Control.Manager
{
    /// <summary>
    /// Interaction logic for StatisticsControl.xaml
    /// </summary>
    public partial class StatisticsControl : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private SeriesCollection _countSeriesCollection;
        public SeriesCollection CountSeriesCollection
        {
            get => _countSeriesCollection;
            set
            {
                _countSeriesCollection = value;
                NotifyPropertyChanged(nameof(CountSeriesCollection));
            }
        }

        private SeriesCollection _priceSeriesCollection;
        public SeriesCollection PriceSeriesCollection
        {
            get => _priceSeriesCollection;
            set
            {
                _priceSeriesCollection = value;
                NotifyPropertyChanged(nameof(PriceSeriesCollection));
            }
        }

        private string[] _labels;
        public string[] Labels
        {
            get => _labels;
            set
            {
                _labels = value;
                NotifyPropertyChanged(nameof(Labels));
            }
        }

        private List<Item> items = new List<Item>();

        private List<NaviData> _naviItems = new List<NaviData>();
        public List<NaviData> NaviItems
        {
            get => _naviItems;
            set
            {
                _naviItems = value;
                NotifyPropertyChanged(nameof(NaviItems));
            }
        }

        public StatisticsControl()
        {
            InitializeComponent();

            SetNaviItems();

            Loaded += StatisticsControl_Loaded;
        }

        private void StatisticsControl_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = this;

            IsVisibleChanged += StatisticsControl_IsVisibleChanged;
        }

        private void StatisticsControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (((bool)e.NewValue) == true)
            {
                lvNavi.SelectedIndex = 0;
            }
            else
            {
                lvNavi.SelectedIndex = -1;
                cbSeat.SelectedIndex = -1;
            }
        }

        private void SetNaviItems()
        {
            NaviItems.Add(new NaviData(EMenu.StatisticsByMenu, "메뉴별"));
            NaviItems.Add(new NaviData(EMenu.StatisticsByCategory, "카테고리별"));
            NaviItems.Add(new NaviData(EMenu.StatisticsBySeat, "좌석별"));
        }

        private void lvNavi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvNavi.SelectedIndex == -1)
            {
                return;
            }

            EMenu selectedMenu = (EMenu)lvNavi.SelectedIndex;

            cbSeat.Visibility = Visibility.Collapsed;
            items.Clear();

            foreach (var orderedItem in App.managerViewModel.OrderedItems)
            {
                switch (selectedMenu)
                {
                    case EMenu.StatisticsByMenu:
                        SetItems(new Func<Item, bool>(x => x.Name == orderedItem.MenuName), orderedItem, orderedItem.MenuName);
                        break;
                    case EMenu.StatisticsByCategory:
                        SetItems(new Func<Item, bool>(x => x.Type == orderedItem.MenuType), orderedItem, orderedItem.MenuType.ToString());
                        break;
                    case EMenu.StatisticsBySeat:
                        cbSeat.Visibility = Visibility.Visible;
                        cbSeat.SelectedIndex = 0;
                        break;
                }
            }

            InitDatas();
        }

        private void SetItems(Func<Item, bool> func, OrderedItem orderedItem, string name)
        {
            Item item = items.Where(func).FirstOrDefault();

            if (item == null)
            {
                items.Add(new Item()
                {
                    Name = name,
                    Type = orderedItem.MenuType,
                    Seat = orderedItem.Seat,
                    Count = orderedItem.Count,
                    Price = orderedItem.Count * orderedItem.MenuPrice
                });
            }
            else
            {
                item.Count += orderedItem.Count;
                item.Price += orderedItem.Count * orderedItem.MenuPrice;
            }
        }

        private void InitDatas()
        {
            SetLabels();
            SetCountSeriesCollection();
            SetPriceSeriesCollection();
        }

        private void SetLabels()
        {
            Labels = new string[items.Count()];

            for (int i = 0; i < items.Count(); i++)
            {
                Labels[i] = items[i].Name;
            }
        }

        private void SetCountSeriesCollection()
        {
            ChartValues<double> chartValues = new ChartValues<double>();

            for (int i = 0; i < items.Count(); i++)
            {
                chartValues.Add(items[i].Count);
            }

            CountSeriesCollection = new SeriesCollection
            {
                new ColumnSeries() {
                    Title = "판매 수 : ",
                    Values = chartValues
                }
            };
        }

        private void SetPriceSeriesCollection()
        {
            ChartValues<double> chartValues = new ChartValues<double>();

            for (int i = 0; i < items.Count(); i++)
            {
                chartValues.Add(items[i].Price);
            }

            PriceSeriesCollection = new SeriesCollection
            {
                new ColumnSeries() {
                    Title = "총액 : ",
                    Values = chartValues
                }
            };
        }

        private void cbSeat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbSeat.SelectedIndex == -1) 
            {
                return;
            }

            items.Clear();

            foreach (var orderedItem in App.managerViewModel.OrderedItems)
            {
                int selectedSeat = cbSeat.SelectedIndex + 1;

                if (orderedItem.Seat != null && orderedItem.Seat == selectedSeat)
                {
                    SetItems(new Func<Item, bool>(x => x.Seat == selectedSeat), orderedItem, $"{selectedSeat}번 좌석");
                }
            }

            InitDatas();
        }
    }

    public class Item
    {
        public string Name { get; set; }
        public ECategory Type { get; set; }
        public int? Seat { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
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
        StatisticsByMenu,
        StatisticsByCategory,
        StatisticsBySeat
    }
}
