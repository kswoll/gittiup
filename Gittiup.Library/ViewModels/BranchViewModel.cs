using LibGit2Sharp;
using Movel.Ears;
using Movel.Utils;

namespace Gittiup.Library.ViewModels
{
    public class BranchViewModel : BaseObject
    {
        public Repository Repository { get; }
        public Branch Branch { get; }
        public Commit SelectedCommit { get; set; }
        public I

        public BranchViewModel(Repository repository, Branch branch)
        {
            Repository = repository;
            Branch = branch;

            this.Listen(x => x.SelectedCommit).Then()
        }

        private string FormatMessage(string commitMessage)
        {
            var markdownedMessage = CommonMark.CommonMarkConverter.Convert(commitMessage);
            return $"<html style=\"font-family: Arial; font-size: 10pt;\">{markdownedMessage}</html>";
        }


    }
}