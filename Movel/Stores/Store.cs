using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Movel.Actions;
using Movel.Utils;

namespace Movel.Stores
{
    public class Store : BaseObject
    {
        public Store()
        {
            ValidateStore();
        }

        private void ValidateStore()
        {
            foreach (var property in GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (property.GetSetMethod() != null)
                {
                    throw new InvalidStoreException($"Stores cannot have public writable properties: {property.DeclaringType.FullName}.{property.Name}");
                }
            }
        }

        public IEnumerable<Store> ChildStores
        {
            get
            {
                foreach (var property in GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(x => typeof(Store).IsAssignableFrom(x.PropertyType) || (x.PropertyType.IsGenericList() && typeof(Store).IsAssignableFrom(x.PropertyType.GetListElementType()))))
                {
                    var value = property.GetValue(this);
                    switch (value)
                    {
                        case Store childStore:
                        {
                            yield return childStore;
                            break;
                        }
                        case IList childStores:
                        {
                            foreach (Store childStore in childStores)
                            {
                                yield return childStore;
                            }
                            break;
                        }
                    }
                }
            }
        }

        public void Dispatch(IDispatcherAction action)
        {
            OnDispatch(action);
            foreach (var childStore in ChildStores)
            {
                childStore.OnDispatch(action);
            }
        }

        protected virtual void OnDispatch(IDispatcherAction action)
        {
        }
    }
}