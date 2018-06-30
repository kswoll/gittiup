using Gittiup.Library.Utils;
using NUnit.Framework;

namespace Gittiup.Library.Tests.Utils
{
    [TestFixture]
    public class DiffLineGeneratorTests
    {
        [Test]
        public void EmptyState()
        {
            var oldContent = "";
            var newContent = "";
            var diffs = DiffLineGenerator.GenerateLineDiffs(oldContent, newContent);
            Assert.AreEqual(1, diffs.Count);
            Assert.AreEqual(0, diffs[0].Count);
        }

        [Test]
        public void InsertNewLine()
        {
            var oldContent = "a\r\nc";
            var newContent = "a\r\nb\r\nc";
            var diffs = DiffLineGenerator.GenerateLineDiffs(oldContent, newContent);
            Assert.AreEqual(3, diffs.Count);
            var line = diffs[1];
            Assert.AreEqual(1, line.Count);
            Assert.AreEqual("b", line[0].Text);
            Assert.IsTrue(line[0].Operation.IsInsert);
        }
    }
}