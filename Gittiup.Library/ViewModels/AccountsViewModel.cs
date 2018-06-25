using System.Collections.ObjectModel;
using Gittiup.Library.Models;
using Gittiup.Library.Utils;

namespace Gittiup.Library.ViewModels
{
    public class AccountsViewModel
    {
        public ObservableCollection<AccountModel> Accounts { get; set; } = new ObservableCollection<AccountModel>();

        public AccountsViewModel()
        {
            using (var db = new GittiupDb())
            {
                Accounts.AddRange(db.Accounts.FindAll());
            }
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