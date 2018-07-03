using Movel.Stores;

namespace Gittiup.Library.Stores
{
    public class ApplicationStore : Store
    {
        public RepositoriesStore Repositories { get; } = new RepositoriesStore();
        public SettingsStore Settings { get; } = new SettingsStore();
    }
}