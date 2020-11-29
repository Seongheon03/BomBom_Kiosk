using BomBom_Kiosk.Model;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace BomBom_Kiosk.ViewModel
{
    public class ManagerViewModel : BindableBase
    {
        private List<MemberModel> _members;
        public List<MemberModel> Members
        {
            get => _members;
            set => SetProperty(ref _members, value);
        }

        private List<MenuModel> _drinks;
        public List<MenuModel> Drinks
        {
            get => _drinks;
            set => SetProperty(ref _drinks, value);
        }

        private MenuModel _selectedDrink;
        public MenuModel SelectedDrink
        {
            get => _selectedDrink;
            set => SetProperty(ref _selectedDrink, value);
        }

        private TimeSpan _usedTime;
        public TimeSpan UsedTime
        {
            get => _usedTime;
            set => SetProperty(ref _usedTime, value);
        }

        private List<OrderedItem> _orderedItems = new List<OrderedItem>();
        public List<OrderedItem> OrderedItems
        {
            get => _orderedItems;
            set => SetProperty(ref _orderedItems, value);
        }

        public void InitData()
        {
            UsedTime = App.dbManager.GetTime();
            Members = App.paymentViewModel.Members;
            Drinks = App.orderViewModel.Drinks;

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
