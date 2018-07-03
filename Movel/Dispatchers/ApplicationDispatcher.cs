using System.Collections.Immutable;
using Movel.Actions;
using Movel.Stores;

namespace Movel.Dispatchers
{
    public class ApplicationDispatcher
    {
        public ImmutableList<Store> Stores { get; set; }

        public void RegisterStore(Store store)
        {
            Stores.Add(store);
        }

        public void Dispatch(IDispatcherAction action)
        {
            foreach (var store in Stores)
            {
                store.Dispatch(action);
            }
        }
    }
}