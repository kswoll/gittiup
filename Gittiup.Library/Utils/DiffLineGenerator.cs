using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using DiffMatchPatch;

namespace Gittiup.Library.Utils
{
    public static class DiffLineGenerator
    {
        public static ImmutableList<DiffLine> GenerateLineDiffs(string oldText, string newText)
        {
            List<Diff> diffs;
            if (oldText == null)
            {
                diffs = new List<Diff>
                {
                    new Diff(newText, Operation.Insert)
                };
            }
            else if (newText == null)
            {
                diffs = new List<Diff>
                {
                    new Diff(oldText, Operation.Delete)
                };
            }
            else
            {
                var differ = DiffMatchPatchModule.Default;
                diffs = differ.DiffMain(oldText, newText);
                differ.DiffCleanupSemantic(diffs);
            }

//            var differ = new DiffMatchPatch.DiffMatchPatch(.5f, 32, 4, 0.5f, 1000, 32, 0.5f, 4);

            var lines = new List<DiffLine>();
            var currentLine = new DiffLine();
            Diff currentLineInitialDiff = null;
            foreach (var diff in diffs)
            {
                var diffLines = diff.Text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                var newLine = false;
                if (currentLine.All(x => x.Text.Length == 0))
                {
                    currentLineInitialDiff = diff;
                }

                foreach (var line in diffLines)
                {
                    if (newLine)
                    {
                        if (currentLineInitialDiff == null || Equals(currentLineInitialDiff, diff))
                        {
                            currentLine.Operation = diff.Operation;
                        }
                        lines.Add(currentLine);
                        currentLine = new DiffLine();
                        currentLineInitialDiff = diff;
                    }

                    if (line.Length > 0)
                    {
                        currentLine.Add(new Diff(line, diff.Operation));
                    }
                    newLine = true;
                }
            }

            if (currentLine.Select(x => x.Operation).ToImmutableHashSet().Count == 1)
                currentLine.Operation = currentLine[0].Operation;
            lines.Add(currentLine);

            return lines.ToImmutableList();
        }
    }
}