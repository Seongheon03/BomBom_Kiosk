using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace BomBom_Kiosk.Service
{
    public class UIManager
    {
        private Dictionary<UICategory, UserControl> uiDic = new Dictionary<UICategory, UserControl>();
        private Stack<UserControl> uiStack = new Stack<UserControl>();

        public void AddUserControl(UICategory category, UserControl userControl)
        {
            uiDic.Add(category, userControl);
        }

        public void Push(UICategory category)
        {
            if (uiStack.Count != 0)
            {
                SetVisible(uiStack.Peek(), Visibility.Collapsed);
            }

            UserControl userControl = uiDic[category];

            uiStack.Push(userControl);
            SetVisible(userControl, Visibility.Visible);
        }

        public void Pop()
        {
            SetVisible(uiStack.Pop(), Visibility.Collapsed);

            if (uiStack.Count != 0)
            {
                SetVisible(uiStack.Peek(), Visibility.Visible);
            }
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
