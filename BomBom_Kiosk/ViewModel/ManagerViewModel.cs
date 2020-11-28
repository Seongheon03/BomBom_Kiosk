using Prism.Mvvm;
using System;
using System.Windows.Threading;

namespace BomBom_Kiosk.ViewModel
{
    public class ManagerViewModel : BindableBase
    {
        private TimeSpan _usedTime;
        public TimeSpan UsedTime
        {
            get => _usedTime;
            set => SetProperty(ref _usedTime, value);
        }

        public void InitData()
        {
            UsedTime = App.dbManager.GetTime();

            SetTimer();
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
            UsedTime += TimeSpan.FromSeconds(1);
        }
    }
}
