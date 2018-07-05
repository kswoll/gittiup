using System.Collections.ObjectModel;
using System.Linq;
using Gittiup.Library.Models;
using Gittiup.Library.Utils;
using Movel.Utils;

namespace Gittiup.Library.ViewModels
{
    public class RepositoriesViewModel : BaseObject
    {
        public ObservableCollection<RepositoryViewModel> Repositories { get; set; } = new ObservableCollection<RepositoryViewModel>();

        public RepositoriesViewModel()
        {
            using (var db = new GittiupDb())
            {
                Repositories.AddRange(db.Repositories.FindAll().Select(x => new RepositoryViewModel(x)));
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
                Repositories.Add(new RepositoryViewModel(repository));
            }
        }
    }
}