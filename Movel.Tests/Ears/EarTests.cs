﻿using Movel.Ears;
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

        private class ClassA : BaseObject
        {
            public string PropertyA { get; set; }
        }
    }
}