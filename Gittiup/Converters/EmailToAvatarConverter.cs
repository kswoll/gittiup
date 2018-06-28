using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using Gittiup.Library.Models;
using Gittiup.Library.Utils;

namespace Gittiup.Converters
{
    public class EmailToAvatarConverter : IValueConverter
    {
        private static readonly GittiupDb db = new GittiupDb();
        private static readonly Dictionary<string, BitmapImage> cache = new Dictionary<string, BitmapImage>();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var email = (string)value;
            email = email.Trim().ToLower();

            if (!cache.TryGetValue(email, out var avatar))
            {
                var user = db.Users.FindOne(x => x.Email == email);
                if (user == null)
                {
                    user = new UserModel();
                    user.Email = email;
                    user.Avatar = GravatarUtils.FetchGravatarImage(email);
                    db.Users.Upsert(user);
                }

                avatar = new BitmapImage();
                avatar.BeginInit();
                avatar.StreamSource = new MemoryStream(user.Avatar);
                avatar.EndInit();
            }

            return avatar;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}