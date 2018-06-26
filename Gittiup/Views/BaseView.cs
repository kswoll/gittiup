using System;
using System.Windows.Controls;

namespace Gittiup.Views
{
    public class BaseView<T> : UserControl
        where T : class
    {
        private T viewModel;

        public BaseView()
        {
        }

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
    }
}