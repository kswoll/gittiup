using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Gittiup.Library.Utils
{
    public static class GravatarUtils
    {
        public static byte[] FetchGravatarImage(string email)
        {
            var client = new WebClient();
            return client.DownloadData(ComputeGravatarUrl(email));
        }

        public static string ComputeGravatarUrl(string email)
        {
            return $"https://www.gravatar.com/avatar/{ComputeGravatarHash(email)}";
        }

        private static string ComputeGravatarHash(string email)
        {
            email = email.Trim().ToLowerInvariant();

            var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(email);
            var hash = md5.ComputeHash(inputBytes);
            var result = string.Join("", hash.Select(x => x.ToString("X2")));
            return result.ToLowerInvariant();
        }
    }
}