using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using Gittiup.Library.Models;
using Gittiup.Library.Utils;
using LibGit2Sharp;
using Movel.Commands;
using Movel.Ears;
using Movel.Utils;
using Patch = LibGit2Sharp.Patch;

namespace Gittiup.Library.ViewModels
{
    public class BranchViewModel : BaseObject
    {
        public Repository Repository { get; }
        public AccountModel Account { get; }
        public Branch Branch { get; }
        public BranchItemViewModel SelectedItemViewModel { get; set; }
        public string SelectedCommitMessage { get; set; }
        public ImmutableList<string> Files { get; set; }
        public string SelectedFile { get; set; }
        public ImmutableList<DiffLine> SelectedFileContent { get; set; }
        public ImmutableList<BranchItemViewModel> Commits { get; set; }
        public IAsyncCommand<Branch> Checkout { get; set; }

        public BranchViewModel(Repository repository, AccountModel account, Branch branch)
        {
            Repository = repository;
            Account = account;
            Branch = branch;

            this.Listen(x => x.SelectedItemViewModel).Then(OnSelectedItemViewModelChanged);
            this.Listen(x => x.SelectedFile).Then(WhenSelectedFileChanged);

            var commits = new List<BranchItemViewModel>();
            if (repository.Head.CanonicalName == branch.CanonicalName)
            {
                var status = repository.RetrieveStatus(new StatusOptions()
                {
                });
                if (status.IsDirty)
                {
                    commits.Add(new BranchItemViewModel
                    {
                        Author = account.Email,
                        Message = "(Working Copy Changes)",
                        Changes = status
                    });
                }
            }

            commits.AddRange(branch.Commits.Select(x => new BranchItemViewModel
            {
                Commit = x,
                Message = x.MessageShort,
                When = x.Author.When.LocalDateTime,
                Author = x.Author.Email
            }));

            Commits = commits.ToImmutableList();
        }

        private void OnSelectedItemViewModelChanged()
        {
            var item = SelectedItemViewModel;
            if (item.Commit != null)
            {
                var commit = item.Commit;
                SelectedCommitMessage = FormatMessage(commit.Message);

                var diff = Repository.Diff.Compare<Patch>(commit.Tree, commit.Parents.FirstOrDefault()?.Tree);
                var paths = diff.Select(x => x.Path).ToArray();
                Files = paths.ToImmutableList();
            }
            else if (item.Changes != null)
            {
                var changes = item.Changes;
                Files = changes.Where(x => x.State != FileStatus.Ignored).Select(x => x.FilePath).ToImmutableList();
                SelectedCommitMessage = "<html></html>";
            }
            else
            {
                SelectedCommitMessage = "<html></html>";
                Files = ImmutableList<string>.Empty;
            }
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

            string oldContentText;
            string newContentText;
            if (SelectedItemViewModel.Commit != null)
            {
                var commit = SelectedItemViewModel.Commit;

                var oldContent = (Blob)commit?.Parents.FirstOrDefault()?[selectedFile]?.Target;
                var newContent = (Blob)commit?[selectedFile]?.Target;

                oldContentText = oldContent?.GetContentText();
                newContentText = newContent?.GetContentText();
            }

            else
            {
                var oldContent = (Blob)Branch.Tip[selectedFile]?.Target;
                oldContentText = oldContent?.GetContentText();

                newContentText = File.ReadAllText(Path.Combine(Repository.Info.WorkingDirectory, selectedFile)).Replace("\r\n", "\n");
            }

            SelectedFileContent = DiffLineGenerator.GenerateLineDiffs(oldContentText, newContentText);
        }

        public void CreateBranch(string branchName)
        {
            Repository.CreateBranch(branchName);
        }
    }
}