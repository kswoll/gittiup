using System;
using System.Collections.Immutable;
using System.Windows.Input;
using Gittiup.Library.Utils;
using Gittiup.Library.ViewModels;
using Movel.Ears;

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

        protected override void OnViewModelChanged(CommitViewModel oldModel, CommitViewModel newModel)
        {
            base.OnViewModelChanged(oldModel, newModel);

            ViewModel.Listen(x => x.SelectedFile).Then(OnSelectedFileChanged);
            comment.NavigateToString(ViewModel.CommitMessage);
        }

        private void Files_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            SelectedFileChanged?.Invoke(ViewModel.SelectedFileContent);
        }

        private void OnSelectedFileChanged(Ear<string> ear, string oldValue, string newValue)
        {
            SelectedFileChanged?.Invoke(ViewModel.SelectedFileContent);
        }
    }
}
