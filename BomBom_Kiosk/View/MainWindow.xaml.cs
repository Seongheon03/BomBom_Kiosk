using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace BomBom_Kiosk.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UserControl currentControl = new UserControl();

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            currentControl = ctrlHome;
            SetBackground();
            SetTimer();

            DataContext = this;
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            ChangeControl(ctrlHome);
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

        private void ChangeControl(UserControl tragetControl)
        {
            currentControl.Visibility = Visibility.Collapsed;
            currentControl = tragetControl;
            currentControl.Visibility = Visibility.Visible;
        }
    }
}
