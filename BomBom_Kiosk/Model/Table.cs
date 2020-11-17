using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Threading;

namespace BomBom_Kiosk.Model
{
    public class Table : BindableBase
    {
        DispatcherTimer timer = new DispatcherTimer();

        public int Number { get; set; }

        private TimeSpan _leftTime = TimeSpan.FromSeconds(60);
        public TimeSpan LeftTime 
        {
            get => _leftTime;
            set => SetProperty(ref _leftTime, value);
        }

        private bool _isUsing;
        public bool IsUsing
        {
            get => _isUsing;
            set
            {
                SetProperty(ref _isUsing, value);

                if (IsUsing)
                {
                    StartTimer();
                    BackColor = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFBA0D");
                }
                else
                {
                    BackColor = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFEA00");
                }
            }
        }

        private SolidColorBrush _backColor = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFEA00");
        public SolidColorBrush BackColor
        {
            get => _backColor;
            set
            {
                SetProperty(ref _backColor, value);
            }
        }

        private void StartTimer()
        {
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            LeftTime -= TimeSpan.FromSeconds(1);

            if (LeftTime == TimeSpan.FromSeconds(0))
            {
                timer.Stop();
                IsUsing = false;
            }
        }
    }
}
