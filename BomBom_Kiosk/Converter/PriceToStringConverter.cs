using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace BomBom_Kiosk.Converter
{
    public class PriceToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //string price = (parameter != null) ? ((int)value * (int)parameter).ToString() : ((int)value).ToString();

            string price = ((int)value).ToString();

            double count = System.Convert.ToDouble(price.Length) / 3;
            int index = price.Length;

            while (count-- > 1)
            {
                index -= 3;
                price = price.Insert(index, ",");
            }

            return price + "원";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
