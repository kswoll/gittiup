using System;
using System.Windows.Controls;
using Movel.Utils;

namespace Gittiup.Views
{
    public class BaseView<T> : UserControl, IDisposableHost
        where T : class
    {
        private readonly DisposableHost disposableHost = new DisposableHost();

        private T viewModel;
        private bool isDisposed;

        public T ViewModel
        {
            get => viewModel;
            set
            {
                if (viewModel != value)
                {
                    (viewModel as IDisposable)?.Dispose();
                    viewModel = value;
                    DataContext = value;
                }
            }
        }

        public void Dispose()
        {
            if (!isDisposed)
            {
                OnDispose();
                isDisposed = true;
            }
        }

        protected virtual void OnDispose()
        {
            disposableHost.Dispose();
            (ViewModel as IDisposable)?.Dispose();
        }

        public void AddDisposable(IDisposable disposable)
        {
            disposableHost.AddDisposable(disposable);
        }

        public void RemoveDisposable(IDisposable disposable)
        {
            disposableHost.RemoveDisposable(disposable);
        }
    }
}