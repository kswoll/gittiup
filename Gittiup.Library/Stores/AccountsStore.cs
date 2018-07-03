using System.Collections.Immutable;
using Gittiup.Library.Models;
using Movel.Ears;
using Movel.Stores;

namespace Gittiup.Library.Stores
{
    public class AccountsStore : Store
    {
        public ImmutableList<AccountModel> Accounts { get; private set; }
        public AccountModel SelectedAccount { get; private set; }
        public bool CanEditSelectedAccount { get; private set; }

        public AccountsStore()
        {
            using (var db = new GittiupDb())
            {
                Accounts = db.Accounts.FindAll().ToImmutableList();
            }

            this.Listen(x => x.SelectedAccount).Then((ear, oldValue, newValue) => CanEditSelectedAccount = newValue != null);
        }

        private void SaveAccount(AccountModel account)
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

        private void DeleteAccount(AccountModel account)
        {
            Accounts.Remove(account);
            using (var db = new GittiupDb())
            {
                db.Accounts.Delete(account.Id);
            }
        }
    }
}