using Gittiup.Library.Models;
using Gittiup.Library.Utils;
using LibGit2Sharp;

namespace Gittiup.Library.ViewModels
{
    public class RepositoryViewModel : BaseObject
    {
        public RepositoryModel Repository { get; }
        public Repository Repo { get; }

        public RepositoryViewModel(RepositoryModel repository)
        {
            Repository = repository;
            Repo = new Repository(repository.Path);
        }
    }
}