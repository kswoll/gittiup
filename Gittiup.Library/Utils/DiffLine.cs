using System.Collections.Generic;
using DiffMatchPatch;

namespace Gittiup.Library.Utils
{
    public class DiffLine : List<Diff>
    {
        public Operation Operation { get; set; }
    }
}