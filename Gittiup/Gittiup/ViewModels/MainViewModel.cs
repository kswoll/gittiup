using System.Collections.ObjectModel;
using Gittiup.Database;
using Gittiup.Utils;

namespace Gittiup.ViewModels
{
    public class MainViewModel
    {
        public ObservableCollection<Repository> Repositories { get; set; } = new ObservableCollection<Repository>();

        private GittiupDb db = new GittiupDb();

        public MainViewModel()
        {
            Repositories.AddRange(db.Repositories.FindAll());
        }
    }
}