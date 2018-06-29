using System.Collections.ObjectModel;
using Gittiup.Library.Models;
using Gittiup.Library.Utils;
using Movel.Ears;
using Movel.Utils;

namespace Gittiup.Library.ViewModels
{
    public class AccountsViewModel : BaseObject
    {
        public ObservableCollection<AccountModel> Accounts { get; set; } = new ObservableCollection<AccountModel>();
        public AccountModel SelectedAccount { get; set; }
        public bool CanEditSelectedAccount { get; set; }

        public AccountsViewModel()
        {
            using (var db = new GittiupDb())
            {
                Accounts.AddRange(db.Accounts.FindAll());
            }

            this.Listen(x => x.SelectedAccount).Then((ear, oldValue, newValue) => CanEditSelectedAccount = newValue != null);
        }

        public void SaveAccount(AccountModel account)
        {
            var isNew = account.Id == 0;

            using (var db = new GittiupDb())
            {
                db.Accounts.Upsert(account);
            }

            if (isNew)
            {
                Accounts.Add(account);
            }
        }

        public void DeleteAccount(AccountModel account)
        {
            Accounts.Remove(account);
            using (var db = new GittiupDb())
            {
                db.Accounts.Delete(account.Id);
            }
        }
    }
}