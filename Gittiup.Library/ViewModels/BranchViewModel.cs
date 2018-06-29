using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using DiffMatchPatch;
using Gittiup.Library.Models;
using Gittiup.Library.Utils;
using LibGit2Sharp;
using Movel.Ears;
using Movel.Utils;
using Diff = DiffMatchPatch.Diff;
using Patch = LibGit2Sharp.Patch;

namespace Gittiup.Library.ViewModels
{
    public class BranchViewModel : BaseObject
    {
        public Repository Repository { get; }
        public Branch Branch { get; }
        public BranchItemViewModel SelectedItemViewModel { get; set; }
        public string SelectedCommitMessage { get; set; }
        public ImmutableList<string> Files { get; set; }
        public string SelectedFile { get; set; }
        public ImmutableList<DiffLine> SelectedFileContent { get; set; }
        public ImmutableList<BranchItemViewModel> Commits { get; set; }

        public BranchViewModel(Repository repository, AccountModel account, Branch branch)
        {
            Repository = repository;
            Branch = branch;

            this.Listen(x => x.SelectedItemViewModel).Then(OnSelectedItemViewModelChanged);
            this.Listen(x => x.SelectedFile).Then(OnSelectedFileChanged);

            var commits = new List<BranchItemViewModel>();
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
            return $"<html style=\"font-family: Arial; font-size: 10pt;\">{markdownedMessage}</html>";
        }

        private void OnSelectedFileChanged()
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

                newContentText = File.ReadAllText(Path.Combine(Repository.Info.WorkingDirectory, selectedFile));
            }

            List<Diff> diffs;
            if (oldContentText == null)
            {
                diffs = new List<Diff>
                {
                    new Diff(newContentText, Operation.Insert)
                };
            }
            else
            {
                var differ = DiffMatchPatchModule.Default;
                diffs = differ.DiffMain(oldContentText, newContentText);
                differ.DiffCleanupSemantic(diffs);
            }

//            var differ = new DiffMatchPatch.DiffMatchPatch(.5f, 32, 4, 0.5f, 1000, 32, 0.5f, 4);

            var lines = new List<DiffLine>();
            var currentLine = new DiffLine();
            foreach (var diff in diffs)
            {
                var diffLines = diff.Text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                var newLine = false;
                foreach (var line in diffLines)
                {
                    if (newLine)
                    {
                        lines.Add(currentLine);
                        currentLine = new DiffLine();
                    }
                    currentLine.Add(new Diff(line, diff.Operation));
                    newLine = true;
                }
            }

            if (currentLine.Any())
            {
                lines.Add(currentLine);
            }

            SelectedFileContent = lines.ToImmutableList();
        }
    }
}