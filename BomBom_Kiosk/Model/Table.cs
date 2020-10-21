using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace BomBom_Kiosk.Model
{
    public class Table
    {
        DispatcherTimer timer = new DispatcherTimer();

        public int Number { get; set; }
        public TimeSpan LeftTime { get; set; } = TimeSpan.FromSeconds(60);
        public bool IsUsing
        {
            get => IsUsing;
            set
            {
                IsUsing = value;

                if (IsUsing)
                {
                    StartTimer();
                }
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
