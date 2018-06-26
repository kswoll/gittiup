using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using DiffMatchPatch;
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
        public Commit SelectedCommit { get; set; }
        public string SelectedCommitMessage { get; set; }
        public ImmutableList<string> Files { get; set; }
        public string SelectedFile { get; set; }
        public ImmutableList<DiffLine> SelectedFileContent { get; set; }

        public BranchViewModel(Repository repository, Branch branch)
        {
            Repository = repository;
            Branch = branch;

            this.Listen(x => x.SelectedCommit).Then(OnSelectedCommitChanged);
            this.Listen(x => x.SelectedFile).Then(OnSelectedFileChanged);
        }

        private void OnSelectedCommitChanged()
        {
            var commit = SelectedCommit;
            if (commit != null)
            {
                SelectedCommitMessage = FormatMessage(commit.Message);

                var diff = Repository.Diff.Compare<Patch>(commit.Tree, commit.Parents.FirstOrDefault()?.Tree);
                var paths = diff.Select(x => x.Path).ToArray();
                Files = paths.ToImmutableList();
            }
            else
            {
                SelectedCommitMessage = "";
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

            var commit = SelectedCommit;

            var oldContent = (Blob)commit.Parents.FirstOrDefault()?[selectedFile]?.Target;
            var newContent = (Blob)commit[selectedFile]?.Target;

            var oldContentText = oldContent?.GetContentText();
            var newContentText = newContent?.GetContentText();

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