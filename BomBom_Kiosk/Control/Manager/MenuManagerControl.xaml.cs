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
            Key inputKey = e.Key.Equals(Key.ImeProcessed) ? e.ImeProcessedKey : e.Key;

            if (!(inputKey >= Key.D0 && inputKey <= Key.D9 || inputKey == Key.Back))
            {
                e.Handled = true;
            }
        }
    }
}
