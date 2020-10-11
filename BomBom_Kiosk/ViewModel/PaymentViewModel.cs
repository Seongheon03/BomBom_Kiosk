using BomBom_Kiosk.Model;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            SetTables();
        }
        private void SetTables()
        {
            Tables.Add(new Table { Number = 1, LeftTime = "" });
            Tables.Add(new Table { Number = 2, LeftTime = "" });
            Tables.Add(new Table { Number = 3, LeftTime = "" });
            Tables.Add(new Table { Number = 4, LeftTime = "" });
            Tables.Add(new Table { Number = 5, LeftTime = "" });
            Tables.Add(new Table { Number = 6, LeftTime = "" });
            Tables.Add(new Table { Number = 7, LeftTime = "" });
            Tables.Add(new Table { Number = 8, LeftTime = "" });
            Tables.Add(new Table { Number = 9, LeftTime = "" });
        }
    }
}
