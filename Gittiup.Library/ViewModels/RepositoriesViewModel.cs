using System.Collections.ObjectModel;
using Gittiup.Library.Models;
using Gittiup.Library.Utils;
using Movel.Utils;

namespace Gittiup.Library.ViewModels
{
    public class RepositoriesViewModel : BaseObject
    {
        public ObservableCollection<RepositoryModel> Repositories { get; set; } = new ObservableCollection<RepositoryModel>();

        public RepositoriesViewModel()
        {
            using (var db = new GittiupDb())
            {
                Repositories.AddRange(db.Repositories.FindAll());
            }
        }

        public void SaveRepository(RepositoryModel repository)
        {
            bool isNew = repository.Id == 0;

            using (var db = new GittiupDb())
            {
                db.Repositories.Upsert(repository);
            }

            if (isNew)
            {
                Repositories.Add(repository);
            }
        }
    }
}