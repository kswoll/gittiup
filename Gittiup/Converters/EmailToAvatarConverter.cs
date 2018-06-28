using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using Gittiup.Library.Utils;

namespace Gittiup.Converters
{
    public class EmailToAvatarConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var email = (string)value;
            var avatarData = GravatarUtils.FetchGravatarImage(email);
            var avatar = new BitmapImage();
            avatar.BeginInit();
            avatar.StreamSource = new MemoryStream(avatarData);
            avatar.EndInit();
            return avatar;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}