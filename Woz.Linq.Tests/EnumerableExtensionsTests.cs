using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Woz.Linq.Tests
{
    [TestClass]
    public class EnumerableExtensionsTests
    {
        private class TestObject
        {
            public int Value { get; set; }
        }

        [TestMethod]
        public void ToEnumerable()
        {
            CollectionAssert.AreEqual(new[] {1}, 1.ToEnumerable().ToArray());
        }

        [TestMethod]
        public void Prepend()
        {
            var source = new[] {2, 3, 4};
            var expected = new[] {1, 2, 3, 4};

            CollectionAssert.AreEqual(
                expected, source.Prepend(1).ToArray());
        }

        [TestMethod]
        public void Concat()
        {
            var source = new[] {2, 3, 4};
            var expected = new[] {2, 3, 4, 5};

            CollectionAssert.AreEqual(
                expected, source.Concat(5).ToArray());
        }

        [TestMethod]
        public void MinOrElseWhenValues()
        {
            Assert.AreEqual(2, new[] {3, 2, 5}.MinOrElse(6));
        }

        [TestMethod]
        public void MinOrElseNoValues()
        {
            Assert.AreEqual(2, new int[0].MinOrElse(2));
        }

        [TestMethod]
        public void MaxOrElseWhenValues()
        {
            Assert.AreEqual(5, new[] {3, 2, 5}.MaxOrElse(6));
        }

        [TestMethod]
        public void MaxOrElseNoValues()
        {
            Assert.AreEqual(2, new int[0].MaxOrElse(2));
        }

        [TestMethod]
        public void MinBy()
        {
            var source =
                new[]
                {
                    new TestObject {Value = 3},
                    new TestObject {Value = 2},
                    new TestObject {Value = 5}
                };

            Assert.AreSame(source[1], source.MinBy(x => x.Value));
        }

        [TestMethod]
        public void MinByOrElseWhenValues()
        {
            var source =
                new[]
                {
                    new TestObject {Value = 3},
                    new TestObject {Value = 2},
                    new TestObject {Value = 5}
                };

            Assert.AreSame(
                source[1],
                source.MinByOrElse(x => x.Value, () => null));
        }

        [TestMethod]
        public void MinByOrElseNoValues()
        {
            var source = new TestObject[0];
            var expected = new TestObject {Value = 3};

            Assert.AreSame(
                expected,
                source.MinByOrElse(x => x.Value, () => expected));
        }

        [TestMethod]
        public void MaxByOrElseWhenValues()
        {
            var source =
                new[]
                {
                    new TestObject {Value = 3},
                    new TestObject {Value = 2},
                    new TestObject {Value = 5}
                };

            Assert.AreSame(
                source[2],
                source.MaxByOrElse(x => x.Value, () => null));
        }

        [TestMethod]
        public void MaxByOrElseNoValues()
        {
            var source = new TestObject[0];
            var expected = new TestObject {Value = 3};

            Assert.AreSame(
                expected,
                source.MaxByOrElse(x => x.Value, () => expected));
        }

        [TestMethod]
        public void DistinctBy()
        {
            var source =
                new[]
                {
                    new {Key = 1},
                    new {Key = 2},
                    new {Key = 1},
                    new {Key = 2}
                };

            CollectionAssert.AreEqual(
                new[] {1, 2}, 
                source.DistinctBy(x => x.Key).Select(x => x.Key).ToArray());
        }


        [TestMethod]
        public void Each()
        {
            var source = new[] {1, 2, 3};
            var resultBuffer = new List<int>();

            source.Each(resultBuffer.Add);

            CollectionAssert.AreEqual(source, resultBuffer);
        }
    }
}