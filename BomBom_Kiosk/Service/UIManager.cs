using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace BomBom_Kiosk.Service
{
    public class UIManager
    {
        public Stack<UserControl> uiStack = new Stack<UserControl>();

        public void Push(UserControl userControl)
        {
            if (uiStack.Count != 0)
            {
                uiStack.Peek().Visibility = Visibility.Collapsed;
            }

            uiStack.Push(userControl);
            SetVisible(userControl, Visibility.Visible);
        }

        public void Pop()
        {
            uiStack.Pop();

            if (uiStack.Count == 0)
            {
                return;
            }

            SetVisible(uiStack.Peek(), Visibility.Collapsed);
        }

        public void SetVisible(UserControl userControl, Visibility visible)
        {
            userControl.Visibility = visible;
        }
    }
}
