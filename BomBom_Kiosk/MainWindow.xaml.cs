using BomBom_Kiosk.Service;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Threading;

namespace BomBom_Kiosk
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = this;
            SetBackground();
            SetTimer();
            InitUIDic();

            App.uiManager.PushUC(UICategory.ORDER);
        }

        private void InitUIDic()
        {
            App.uiManager.AddUC(UICategory.HOME, ctrlHome);
            App.uiManager.AddUC(UICategory.MANAGER, ctrlManager);
            App.uiManager.AddUC(UICategory.ORDER, ctrlOrder);
            App.uiManager.AddUC(UICategory.CHOOSEPLACE, ctrlChoosePlace);
            App.uiManager.AddUC(UICategory.INSHOP, ctrlInShop);
            App.uiManager.AddUC(UICategory.CHOOSEPAYMENT, ctrlChoosePayment);
            App.uiManager.AddUC(UICategory.PAYMENTBYCASH, ctrlPaymentByCash);
            App.uiManager.AddUC(UICategory.PAYMENTBYCARD, ctrlPaymentByCard);
            App.uiManager.AddUC(UICategory.PAYMENTRESULT, ctrlPaymentByResult);
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            App.uiManager.PushUC(UICategory.HOME);
        }

        private void SetBackground()
        {
            var desktopWorkingArea = SystemParameters.WorkArea;

            this.Top = desktopWorkingArea.Top;
            this.Left = desktopWorkingArea.Left;
            this.Height = desktopWorkingArea.Height;
            this.Width = desktopWorkingArea.Width;
        }

        private void SetTimer()
        {
            DispatcherTimer timer = new DispatcherTimer();

            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            SetTime();
        }

        private void SetTime()
        {
            tbCurrentTime.Text = DateTime.Now.ToString(string.Format("MM월 dd일 (ddd) tt hh시 mm분", CultureInfo.CreateSpecificCulture("ko-KR")));
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (App.uiManager.GetCurrentUC() == ctrlHome && e.Key == System.Windows.Input.Key.F2)
            {
                App.uiManager.PushUC(UICategory.MANAGER);
            }
        }
    }
}
