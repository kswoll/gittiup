using System.Collections.ObjectModel;
using Gittiup.Library.Models;
using Gittiup.Library.Utils;

namespace Gittiup.Library.ViewModels
{
    public class EditRepositoryViewModel
    {
        public RepositoryModel Repository { get; set; }
        public ObservableCollection<AccountModel> Accounts { get; } = new ObservableCollection<AccountModel>();

        public EditRepositoryViewModel(RepositoryModel repository)
        {
            Repository = repository;

            using (var db = new GittiupDb())
            {
                var accounts = db.Accounts.FindAll();
                Accounts.AddRange(accounts);
            }
        }
    }
}