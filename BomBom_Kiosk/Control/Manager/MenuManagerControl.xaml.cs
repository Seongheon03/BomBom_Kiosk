using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace BomBom_Kiosk.Control.Manager
{
    /// <summary>
    /// Interaction logic for MenuManagerControl.xaml
    /// </summary>
    public partial class MenuManagerControl : UserControl
    {
        public MenuManagerControl()
        {
            InitializeComponent();
        }

        private void tbDiscount_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!(e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key == Key.Back))
            {
                e.Handled = true;
            }
        }
    }
}
