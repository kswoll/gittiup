using System;
using LibGit2Sharp;

namespace Gittiup.Library.ViewModels
{
    public class CommitNodeItemViewModel : NodeItemViewModel
    {
        public Commit Commit { get; set; }
        public string Message { get; set; }
        public DateTime? When { get; set; }
        public string Author { get; set; }
    }
}