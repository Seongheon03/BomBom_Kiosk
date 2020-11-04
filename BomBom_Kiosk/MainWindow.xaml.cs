﻿using BomBom_Kiosk.Service;
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
        TimeSpan driving_time = new TimeSpan(0, 0, 0);
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            App.orderViewModel.LoadingAction += OrderViewModel_LoadingAction;
            App.orderViewModel.InitData();

            DataContext = this;

            SetBackground();
            SetTimer();
            InitUIDic();

            App.uiManager.PushUC(UICategory.HOME);
        }

        private void OrderViewModel_LoadingAction(object sender, bool isLoading)
        {
            progressRing.IsActive = isLoading;

            if (!isLoading)
            {
                ctrlHome.tbStatus.Text = "환영합니다. 주문을 원하시면 아래 주문하기 버튼을 클릭해주세요.";
                ctrlHome.btnOrder.IsEnabled = true;
            }
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
            if (App.orderViewModel.OrderList.Count != 0)
            {
                if (MessageBox.Show("주문을 취소하시겠습니까?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    App.orderViewModel.OrderList.Clear();
                }
                else
                {
                    return;
                }
            }

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
            TimeSpan duration = new System.TimeSpan(0, 0, 1);
            driving_time = driving_time.Add(duration);
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
