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

        private OrderedItem _orderInfo = new OrderedItem();
        public OrderedItem OrderInfo
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
                    OrderInfo.Seat = SelectedTable.Number;
                }
            }
        }

        private string _orderNumber;
        public string OrderNumber
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

        public bool IsExistMember(EOrderType type, string code)
        {
            MemberModel member = new MemberModel();

            switch (type)
            {
                case EOrderType.Card:
                    member = Members.Where(x => x.QRCode.Trim() == code).FirstOrDefault();
                    break;
                case EOrderType.Cash:
                    member = Members.Where(x => x.Barcode.Trim() == code).FirstOrDefault();
                    break;
            }

            if (member == null)
            {
                return false;
            }

            switch (type)
            {
                case EOrderType.Card:
                    OrderInfo.OrderCode = member.QRCode;
                    break;
                case EOrderType.Cash:
                    OrderInfo.OrderCode = member.Barcode;
                    break;
            }

            OrderInfo.Member.Idx = member.Idx;
            OrderInfo.Member.Name = member.Name;

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
