using System.Collections.Immutable;
using Gittiup.Library.Models;
using LibGit2Sharp;
using Movel.Stores;

namespace Gittiup.Library.Stores
{
    public class RepositoriesStore : Store
    {
        public ImmutableList<RepositoryModel> Repositories { get; private set; }
        public RepositoryStore SelectedRepository { get; private set; }

        public RepositoriesStore()
        {
            using (var db = new GittiupDb())
            {
                Repositories = db.Repositories.FindAll().ToImmutableList();
            }
        }

        private void SaveRepository(RepositoryModel repository)
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