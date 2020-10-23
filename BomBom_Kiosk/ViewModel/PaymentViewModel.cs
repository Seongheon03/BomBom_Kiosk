using BomBom_Kiosk.Model;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace BomBom_Kiosk.ViewModel
{
    public class PaymentViewModel : BindableBase
    {
        private List<Table> _tables = new List<Table>();
        public List<Table> Tables
        {
            get => _tables;
            set => SetProperty(ref _tables, value);
        }
        public PaymentViewModel()
        {
            InitCommand();
            SetTables();
        }

        private void InitCommand()
        {
            ChooseTableCommand = new DelegateCommand<int?>(ChooseTable);
        }

        private void ChooseTable(int? tableNumber)
        {
            DispatcherTimer timer = new DispatcherTimer();

        }

        private void SetTables()
        {
            Tables.Add(new Table { Number = 1 });
            Tables.Add(new Table { Number = 2 });
            Tables.Add(new Table { Number = 3 });
            Tables.Add(new Table { Number = 4 });
            Tables.Add(new Table { Number = 5 });
            Tables.Add(new Table { Number = 6 });
            Tables.Add(new Table { Number = 7 });
            Tables.Add(new Table { Number = 8 });
            Tables.Add(new Table { Number = 9 });
        }

        private void setOrderData(EOrderPlace place, EOrderType type, int table)
        {
            orderData.Place = place;
            orderData.Type = type;
            orderData.Table = table;
        }
    }
}
