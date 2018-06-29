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
            if (operation == null)
            {
                return "Orange";
            }
            else if (operation.IsEqual)
            {
                return "#33FFFFFF";
            }
            else if (operation.IsInsert)
            {
                return "#3300FF00";
            }
            else if (operation.IsDelete)
            {
                return "#33FF0000";
            }
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}