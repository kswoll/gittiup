using System;
using System.Collections.Immutable;
using System.Linq;
using Gittiup.Library.Utils;
using LibGit2Sharp;
using Movel.Ears;
using Movel.Utils;

namespace Gittiup.Library.ViewModels
{
    public class CommitViewModel : BaseObject
    {
        public Repository Repository { get; }
        public Commit Commit { get; }
        public string CommitMessage { get; }
        public bool IsWipVisible { get; }
        public bool IsCommentVisible { get; }
        public ImmutableList<string> Files { get; set; }
        public string SelectedFile { get; set; }
        public ImmutableList<DiffLine> SelectedFileContent { get; set; }

        public CommitViewModel(Repository repository, Commit commit)
        {
            Repository = repository;
            Commit = commit;
            CommitMessage = FormatMessage(commit.Message);
            IsWipVisible = commit.Message.Trim().Equals("wip", StringComparison.InvariantCultureIgnoreCase);
            IsCommentVisible = !IsWipVisible;

            var diff = Repository.Diff.Compare<Patch>(commit.Tree, commit.Parents.FirstOrDefault()?.Tree);
            var paths = diff.Select(x => x.Path).ToArray();
            Files = paths.ToImmutableList();

            this.Listen(x => x.SelectedFile).Then(WhenSelectedFileChanged);
        }

        private string FormatMessage(string commitMessage)
        {
            var markdownedMessage = CommonMark.CommonMarkConverter.Convert(commitMessage);
            return $"<html style=\"font-family: Arial; font-size: 10pt;\"><head><meta charset=\"UTF-8\"></head><body>{markdownedMessage}</body></html>";
        }

        /// <summary>
        /// Don't rename to OnSelectedFileChanged as that messes things up with NotifyPropertyChanged.Fody
        /// </summary>
        private void WhenSelectedFileChanged()
        {
            var selectedFile = SelectedFile;

            if (selectedFile == null)
            {
                return;
            }

            var oldContent = (Blob)Commit?.Parents.FirstOrDefault()?[selectedFile]?.Target;
            var newContent = (Blob)Commit?[selectedFile]?.Target;

            var oldContentText = oldContent?.GetContentText();
            var newContentText = newContent?.GetContentText();

            SelectedFileContent = DiffLineGenerator.GenerateLineDiffs(oldContentText, newContentText);
        }
    }
}