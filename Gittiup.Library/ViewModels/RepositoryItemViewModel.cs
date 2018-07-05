using System.Collections.Immutable;
using Movel.Utils;

namespace Gittiup.Library.ViewModels
{
    public class RepositoryItemViewModel : BaseObject
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public bool IsSelected { get; set; }
        public bool IsCheckedOut { get; set; }
        public bool IsExpanded { get; set; }
        public ImmutableList<RepositoryItemViewModel> Children { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}