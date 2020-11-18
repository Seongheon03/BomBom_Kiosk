using BomBom_Kiosk.Model;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Linq;

namespace BomBom_Kiosk.ViewModel
{
    public class PaymentViewModel : BindableBase
    {
        public List<MemberModel> Members { get; set; } = new List<MemberModel>();

        public List<Table> Tables { get; set; } = new List<Table>();

        private OrderData _orderInfo = new OrderData();
        public OrderData OrderInfo
        {
            get => _orderInfo;
            set => SetProperty(ref _orderInfo, value);
        }

        private Table _selectedTable;
        public Table SelectedTable
        {
            get => _selectedTable;
            set
            {
                _selectedTable = value;

                if (SelectedTable != null)
                {
                    OrderInfo.Table = SelectedTable.Number;
                }
            }
        }

        private int _orderNumber;
        public int OrderNumber
        {
            get => _orderNumber;
            set => SetProperty(ref _orderNumber, value);
        }

        public PaymentViewModel()
        {
            SetTables();
        }

        public void InitMembers()
        {
            Members = App.dbManager.GetMembers();
        }

        public bool IsExistMember(string code)
        {
            MemberModel member = Members.Where(x => x.Code.Trim() == code).FirstOrDefault();

            if (member == null)
            {
                return false;
            }

            OrderInfo.Code = member.Code;
            OrderInfo.Name = member.Name;

            return true;
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
    }
}
