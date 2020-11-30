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

        private List<OrderedItem> items = new List<OrderedItem>();

        private List<StatisticsNaviData> _statisticsNavi = new List<StatisticsNaviData>();
        public List<StatisticsNaviData> StatisticsNavi
        {
            get => _statisticsNavi;
            set
            {
                _statisticsNavi = value;
                NotifyPropertyChanged(nameof(StatisticsNavi));
            }
        }

        public StatisticsControl()
        {
            InitializeComponent();

            SetNaviItems();
            SetCbSeat();

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
            StatisticsNavi.Add(new StatisticsNaviData(StatisticsMenu.ByMenu, "메뉴별"));
            StatisticsNavi.Add(new StatisticsNaviData(StatisticsMenu.ByCategory, "카테고리별"));
            StatisticsNavi.Add(new StatisticsNaviData(StatisticsMenu.BySeat, "좌석별"));
        }

        private void SetCbSeat()
        {
            foreach(var table in App.paymentViewModel.Tables)
            {
                cbSeat.Items.Add($"{table.Number}번 좌석");
            }
        }

        private void lvNavi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvNavi.SelectedIndex == -1)
            {
                return;
            }

            StatisticsMenu selectedMenu = (StatisticsMenu)lvNavi.SelectedIndex;

            cbSeat.Visibility = Visibility.Collapsed;
            items.Clear();

            foreach (var orderedItem in App.managerViewModel.OrderedItems)
            {
                switch (selectedMenu)
                {
                    case StatisticsMenu.ByMenu:
                        SetItems(new Func<OrderedItem, bool>(x => x.MenuName == orderedItem.MenuName), orderedItem, orderedItem.MenuName);
                        break;
                    case StatisticsMenu.ByCategory:
                        SetItems(new Func<OrderedItem, bool>(x => x.MenuType == orderedItem.MenuType), orderedItem, orderedItem.MenuType.ToString());
                        break;
                    case StatisticsMenu.BySeat:
                        cbSeat.Visibility = Visibility.Visible;
                        cbSeat.SelectedIndex = 0;
                        break;
                }
            }

            InitDatas();
        }

        private void SetItems(Func<OrderedItem, bool> func, Model.OrderedItem orderedItem, string name)
        {
            OrderedItem item = items.Where(func).FirstOrDefault();

            if (item == null)
            {
                items.Add(new OrderedItem()
                {
                    MenuName = name,
                    MenuType = orderedItem.MenuType,
                    Seat = orderedItem.Seat,
                    Count = orderedItem.Count,
                    TotalPrice = orderedItem.Count * orderedItem.MenuPrice
                });
            }
            else
            {
                item.Count += orderedItem.Count;
                item.TotalPrice += orderedItem.Count * orderedItem.MenuPrice;
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
                Labels[i] = items[i].MenuName;
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
                chartValues.Add(items[i].TotalPrice);
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
                    SetItems(new Func<OrderedItem, bool>(x => x.Seat == selectedSeat), orderedItem, $"{selectedSeat}번 좌석");
                }
            }

            InitDatas();
        }
    }

    public class StatisticsNaviData
    {
        public StatisticsMenu Idx { get; set; }
        public string Title { get; set; }

        public StatisticsNaviData(StatisticsMenu Idx, string Title)
        {
            this.Idx = Idx;
            this.Title = Title;
        }
    }

    public enum StatisticsMenu
    {
        ByMenu,
        ByCategory,
        BySeat
    }
}
