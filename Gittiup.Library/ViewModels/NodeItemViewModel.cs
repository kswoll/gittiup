using System;
using LibGit2Sharp;
using Movel.Utils;

namespace Gittiup.Library.ViewModels
{
    public abstract class NodeItemViewModel : BaseObject
    {
        public string Message { get; protected set; }
        public DateTime? When { get; protected set; }
        public string Author { get; protected set; }
    }
}