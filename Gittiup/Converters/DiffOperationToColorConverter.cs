using System;
using System.Globalization;
using System.Windows.Data;
using DiffMatchPatch;

namespace Gittiup.Converters
{
    public class DiffOperationToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var operation = (Operation)value;
            if (operation.IsEqual)
            {
                return "#FFFFFF";
            }
            else if (operation.IsInsert)
            {
                return "#00FF00";
            }
            else if (operation.IsDelete)
            {
                return "#FF0000";
            }
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}