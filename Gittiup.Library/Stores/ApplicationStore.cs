using System.Collections.Immutable;
using Movel.Utils;

namespace Gittiup.Library.Stores
{
    public class ApplicationStore : BaseObject
    {
        public ImmutableList<WindowStore> Windows { get; set; }
    }
}