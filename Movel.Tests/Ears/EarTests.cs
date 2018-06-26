using Movel.Ears;
using Movel.Utils;
using NUnit.Framework;

namespace Movel.Tests.Ears
{
    [TestFixture]
    public class EarTests
    {
        [Test]
        public void OnePropertyChanged()
        {
            var a = new ClassA
            {
                PropertyA = "foo"
            };
            var ear = a.Listen(x => x.PropertyA);
            Assert.AreEqual("foo", ear.Value);

            string changed = null;
            ear.ValueChanged += (_, oldValue, newValue) => changed = newValue;
            a.PropertyA = "bar";

            Assert.AreEqual("bar", changed);
            Assert.AreEqual("bar", ear.Value);
        }

        [Test]
        public void ChainedPropertyChanged()
        {
            var a1 = new ClassA
            {
                PropertyA = "foo"
            };
            var a2 = new ClassA
            {
                PropertyA = "baz"
            };
            var b = new ClassB
            {
                ClassProperty = a1
            };

            var ear = b.Listen(x => x.ClassProperty.PropertyA);
            Assert.AreEqual("foo", ear.Value);

            string changed = null;
            ear.ValueChanged += (_, oldValue, newValue) => changed = newValue;
            a1.PropertyA = "bar";

            Assert.AreEqual("bar", changed);
            Assert.AreEqual("bar", ear.Value);

            b.ClassProperty = null;
            Assert.IsNull(changed);
            Assert.IsNull(ear.Value);

            b.ClassProperty = a2;
            Assert.AreEqual("baz", changed);
            Assert.AreEqual("baz", ear.Value);
        }

        private class ClassA : BaseObject
        {
            public string PropertyA { get; set; }
        }

        private class ClassB : BaseObject
        {
            public ClassA ClassProperty { get; set; }
        }
    }
}