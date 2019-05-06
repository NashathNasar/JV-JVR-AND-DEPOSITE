using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;

namespace LookUpEditTokenStyleTest.Views
{
    /// <summary>
    /// Interaction logic for SignsJobCardView
    /// </summary>
    public partial class SignsJobCardView : UserControl
    {
        public SignsJobCardView()
        {
            InitializeComponent();
        }
    }

    public class AluminumSheetSelectionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
           
            var items = value?.ToString().Split(',');
            return items?.OfType<object>().ToList();
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (value is List<object> items)
            {
                var stringBuilder = new StringBuilder();
                foreach (var item in items)
                {
                    stringBuilder.Append(item).Append(",");
                }

                var result = stringBuilder.ToString().TrimEnd(',');
                return result;
            }

            return null;
            
        }
    }
}
