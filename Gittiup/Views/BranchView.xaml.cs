using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DiffMatchPatch;
using Gittiup.Utils;
using Gittiup.ViewModels;
using LibGit2Sharp;
using Diff = DiffMatchPatch.Diff;
using Patch = LibGit2Sharp.Patch;

namespace Gittiup.Views
{
    public class BranchViewBase : BaseView<BranchViewModel>
    {
    }

    public partial class BranchView
    {
        public BranchView(Repository repository, Branch branch)
        {
            InitializeComponent();

            ViewModel = new BranchViewModel(repository, branch);
//            branch.Commits.ElementAt(0).
        }

        private void CloseFile_Click(object sender, RoutedEventArgs e)
        {
            commits.Visibility = Visibility.Visible;
            fileView.Visibility = Visibility.Collapsed;
        }

        private void Commits_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (commits.SelectedItem != null)
            {
                var commit = (Commit)commits.SelectedItem;

                var diff = ViewModel.Repository.Diff.Compare<Patch>(commit.Tree, commit.Parents.First().Tree);
                var paths = diff.Select(x => x.Path).ToArray();
                files.ItemsSource = paths;
                if (rightColumn.ActualWidth == 0)
                {
                    splitterColumn.Width = new GridLength(5);
                    rightColumn.Width = new GridLength(200);
                }
            }
            else
            {
                if (rightColumn.ActualWidth != 0)
                {
                    splitterColumn.Width = new GridLength(0);
                    rightColumn.Width = new GridLength(0);
                }
            }
        }

        private void Files_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectFile();
        }

        private void Files_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (commits.Visibility != Visibility.Collapsed)
            {
                SelectFile();
            }
        }

        private void SelectFile()
        {
            commits.Visibility = Visibility.Collapsed;
            fileView.Visibility = Visibility.Visible;

            var path = (string)files.SelectedItem;
//            var blob = (Blob)entry.Target;
            var history = ViewModel.Repository.Commits.QueryBy(path).ToArray();
            LogEntry current = null;
            LogEntry previous = null;
            var foo = ViewModel.Repository.Commits.Select(x => x.Tree).ToArray();

            var commit = (Commit)commits.SelectedItem;

/*
            foreach (var version in history)
            {
                if (version.Commit.Sha == commit.Sha)
                {
                    current = version;
                }
                else if (current != null)
                {
                    previous = version;
                    break;
                }
            }
*/

            var oldContent = (Blob)commit.Parents.FirstOrDefault()?[path]?.Target;
//            var oldContent = (Blob)previous?.Commit[path].Target;
            var newContent = (Blob)commit[path].Target;
//            var newContent = (Blob)current.Commit[path].Target;

            var oldContentText = oldContent?.GetContentText();
            var newContentText = newContent.GetContentText();

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

            file.ItemsSource = lines;

/*
            foreach (var line in diffs.Lines)
            {
                line.
            }
*/
        }
    }
}
