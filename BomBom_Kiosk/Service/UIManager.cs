using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace BomBom_Kiosk.Service
{
    public class UIManager
    {
        // UC = UserControl
        private Dictionary<UICategory, UserControl> ucDic = new Dictionary<UICategory, UserControl>();
        private Stack<UserControl> ucStack = new Stack<UserControl>();

        public void AddUC(UICategory category, UserControl userControl)
        {
            ucDic.Add(category, userControl);
        }

        public UserControl GetCurrentUC()
        {
            return ucStack.Peek();
        }

        public void PushUC(UICategory category)
        {
            if (ucStack.Count != 0)
            {
                SetVisible(ucStack.Peek(), Visibility.Collapsed);
            }

            UserControl currentUC = ucDic[category];

            ucStack.Push(currentUC);
            SetVisible(currentUC, Visibility.Visible);
        }

        public void PopUC()
        {
            SetVisible(ucStack.Pop(), Visibility.Collapsed);
            SetVisible(ucStack.Peek(), Visibility.Visible);
        }

        public void SetVisible(UserControl ctrl, Visibility visible)
        {
            ctrl.Visibility = visible;
        }
    }

    public enum UICategory
    {
        HOME,
        ORDER,
        CHOOSEPLACE,
        INSHOP,
        CHOOSEPAYMENT,
        PAYMENTBYCASH,
        PAYMENTBYCARD,
        PAYMENTRESULT,
    }
}
