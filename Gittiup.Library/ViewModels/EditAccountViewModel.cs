using System.Collections.ObjectModel;
using Gittiup.Library.Models;

namespace Gittiup.Library.ViewModels
{
    public class EditAccountViewModel
    {
        public ObservableCollection<AccountModel> Accounts { get; set; }
        public AccountModel Account { get; set; }
    }
}