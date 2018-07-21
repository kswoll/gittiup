using System;
using System.Collections.Immutable;
using System.Windows.Input;
using Gittiup.Library.Utils;
using Gittiup.Library.ViewModels;
using Movel.Ears;

namespace Gittiup.Views
{
    public class ChangesViewBase : BaseView<ChangesViewModel>
    {
    }

    public partial class ChangesView
    {
        public event Action<ImmutableList<DiffLine>> SelectedFileChanged;

        public ChangesView()
        {
            InitializeComponent();
        }

        protected override void OnViewModelChanged(ChangesViewModel oldModel, ChangesViewModel newModel)
        {
            base.OnViewModelChanged(oldModel, newModel);

            ViewModel.Listen(x => x.SelectedStagedFile).Then(OnSelectedFileChanged);
            ViewModel.Listen(x => x.SelectedUnstagedFile).Then(OnSelectedFileChanged);
        }

        private void Files_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (Equals(stagedFiles, sender))
                unstagedFiles.SelectedItem = null;
            else
                stagedFiles.SelectedItem = null;

            SelectedFileChanged?.Invoke(ViewModel.SelectedFileContent);
        }

        private void OnSelectedFileChanged(Ear<string> ear, string oldValue, string newValue)
        {
            SelectedFileChanged?.Invoke(ViewModel.SelectedFileContent);
        }
    }
}
