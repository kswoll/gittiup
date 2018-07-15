using System;
using System.Collections.Immutable;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Gittiup.Library.Utils;
using Gittiup.Library.ViewModels;

namespace Gittiup.Views
{
    public class CommitViewBase : BaseView<CommitViewModel>
    {
    }

    public partial class CommitView
    {
        public event Action<ImmutableList<DiffLine>> SelectedFileChanged;

        public CommitView()
        {
            InitializeComponent();
        }

        private void Files_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (commits.Visibility != Visibility.Collapsed)
            {
                ShowSelectedFile();
            }
        }


    }
}
