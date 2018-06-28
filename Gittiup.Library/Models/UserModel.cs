using Movel.Utils;

namespace Gittiup.Library.Models
{
    public class UserModel : BaseObject
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public byte[] Avatar { get; set; }
    }
}