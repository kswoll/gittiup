using System;
using LibGit2Sharp;

namespace Gittiup.Library.ViewModels
{
    public class BranchItemViewModel
    {
        public Commit Commit { get; set; }
        public RepositoryStatus Changes { get; set; }
        public string Message { get; set; }
        public DateTime? When { get; set; }
        public string Author { get; set; }
        public bool IsCommittable => Changes != null;
    }
}