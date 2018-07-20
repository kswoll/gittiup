using System.Collections.Immutable;
using System.IO;
using System.Linq;
using Gittiup.Library.Utils;
using LibGit2Sharp;
using Movel.Ears;
using Movel.Utils;

namespace Gittiup.Library.ViewModels
{
    public class ChangesViewModel : BaseObject
    {
        public Repository Repository { get; }
        public RepositoryStatus Status { get; }
        public string CommitMessage { get; set; }
        public ImmutableList<string> UnstagedFiles { get; set; }
        public ImmutableList<string> StagedFiles { get; set; }
        public string SelectedStagedFile { get; set; }
        public string SelectedUnstagedFile { get; set; }
        public ImmutableList<DiffLine> SelectedFileContent { get; set; }

        public ChangesViewModel(Repository repository, RepositoryStatus status)
        {
            Repository = repository;
            Status = status;

            UnstagedFiles = status.Untracked.Concat(status.Missing).Concat(status.Modified).Select(x => x.FilePath).ToImmutableList();
            StagedFiles = status.Staged.Select(x => x.FilePath).ToImmutableList();

            this.Listen(x => x.SelectedStagedFile).Then(WhenSelectedStagedFileChanged);
            this.Listen(x => x.SelectedUnstagedFile).Then(WhenSelectedUnstagedFileChanged);
        }

        /// <summary>
        /// Don't rename to OnSelectedFileChanged as that messes things up with NotifyPropertyChanged.Fody
        /// </summary>
        private void WhenSelectedStagedFileChanged()
        {
            var selectedFile = SelectedStagedFile;

            if (selectedFile == null)
            {
                return;
            }

            var oldContent = Repository.Lookup<Blob>(Repository.Index[selectedFile].Id);
//                var oldContent = (Blob)Branch.Tip[selectedFile]?.Target;
            var oldContentText = oldContent?.GetContentText();

            var newContentText = File.ReadAllText(Path.Combine(Repository.Info.WorkingDirectory, selectedFile)).Replace("\r\n", "\n");

            SelectedFileContent = DiffLineGenerator.GenerateLineDiffs(oldContentText, newContentText);
        }

        private void WhenSelectedUnstagedFileChanged()
        {
            var selectedFile = SelectedUnstagedFile;

            if (selectedFile == null)
            {
                return;
            }

            var oldContent = Repository.Lookup<Blob>(Repository.Index[selectedFile].Id);
//                var oldContent = (Blob)Branch.Tip[selectedFile]?.Target;
            var oldContentText = oldContent?.GetContentText();

            var newContentText = File.ReadAllText(Path.Combine(Repository.Info.WorkingDirectory, selectedFile)).Replace("\r\n", "\n");

            SelectedFileContent = DiffLineGenerator.GenerateLineDiffs(oldContentText, newContentText);
        }
    }
}