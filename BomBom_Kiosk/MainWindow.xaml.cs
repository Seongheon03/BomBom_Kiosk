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

            App.uiManager.Push(Service.UICategory.ORDER);
        }

        private void InitUIDic()
        {
            App.uiManager.AddUserControl(Service.UICategory.HOME, ctrlHome);
            App.uiManager.AddUserControl(Service.UICategory.ORDER, ctrlOrder);
            App.uiManager.AddUserControl(Service.UICategory.CHOOSEPLACE, ctrlChoosePlace);
            App.uiManager.AddUserControl(Service.UICategory.INSHOP, ctrlInShop);
            App.uiManager.AddUserControl(Service.UICategory.CHOOSEPAYMENT, ctrlChoosePayment);
            App.uiManager.AddUserControl(Service.UICategory.PAYMENTBYCASH, ctrlPaymentByCash);
            App.uiManager.AddUserControl(Service.UICategory.PAYMENTBYCARD, ctrlPaymentByCard);
            App.uiManager.AddUserControl(Service.UICategory.PAYMENTRESULT, ctrlPaymentByResult);
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            App.uiManager.Push(Service.UICategory.HOME);
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
    }
}
