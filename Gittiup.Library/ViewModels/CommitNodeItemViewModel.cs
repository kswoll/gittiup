using System;
using LibGit2Sharp;

namespace Gittiup.Library.ViewModels
{
    public class CommitNodeItemViewModel : NodeItemViewModel
    {
        public Commit Commit { get; }

        public CommitNodeItemViewModel(Commit commit)
        {
            Commit = commit;

            Message = commit.MessageShort;
            When = commit.Author.When.LocalDateTime;
            Author = commit.Author.Email;
        }
    }
}