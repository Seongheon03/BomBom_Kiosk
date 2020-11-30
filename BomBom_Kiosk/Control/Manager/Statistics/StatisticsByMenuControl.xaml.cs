using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;

namespace BomBom_Kiosk.Control.Manager.Statistics
{
    /// <summary>
    /// Interaction logic for StatisticsByMenuControl.xaml
    /// </summary>
    public partial class StatisticsByMenuControl : UserControl, INotifyPropertyChanged
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
        public Func<double, string> Formatter { get; set; }

        public StatisticsByMenuControl()
        {
            InitializeComponent();

            Loaded += StatisticsByMenuControl_Loaded;
        }

        private void StatisticsByMenuControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            DataContext = this;

            IsVisibleChanged += StatisticsByMenuControl_IsVisibleChanged;
        }

        private void StatisticsByMenuControl_IsVisibleChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            if (((bool)e.NewValue) == true)
            {
                SetItems();
                SetCountSeriesCollection();
                SetPriceSeriesCollection();
            }
        }

        private void SetItems()
        {
            items.Clear();

            foreach (var orderedItem in App.managerViewModel.OrderedItems)
            {
                Item item = items.Where(x => x.Name == orderedItem.MenuName).FirstOrDefault();

                if (item == null)
                {
                    items.Add(new Item()
                    {
                        Name = orderedItem.MenuName,
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
    }

    public class Item
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
    }
}
