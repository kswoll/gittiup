using Gittiup.Library.Utils;

namespace Gittiup.Library.Models
{
    public class AccountModel : BaseObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}