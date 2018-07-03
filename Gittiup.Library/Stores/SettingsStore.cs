using Movel.Stores;

namespace Gittiup.Library.Stores
{
    public class SettingsStore : Store
    {
        public AccountsStore Accounts { get; } = new AccountsStore();
    }
}