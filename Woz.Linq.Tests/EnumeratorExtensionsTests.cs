using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Woz.Linq.Tests
{
    [TestClass]
    public class EnumeratorExtensionsTests
    {
        [TestMethod]
        public void ToArray()
        {
            var source = new[] {1, 2, 3, 4};

            var result = source.GetEnumerator().ToArray<int>();

            CollectionAssert.AreEqual(source, result);
        }

        [TestMethod]
        public void ToArrayGeneric()
        {
            var source = new[] {1, 2, 3, 4};

            var result = source.Cast<int>().GetEnumerator().ToArray();

            CollectionAssert.AreEqual(source, result);
        }

        [TestMethod]
        public void ToEnumerable()
        {
            var source = new[] {1, 2, 3, 4};

            var result = source.GetEnumerator().ToEnumerable<int>();

            CollectionAssert.AreEqual(source, result.ToArray());
        }

        [TestMethod]
        public void ToEnumerableGeneric()
        {
            var source = new[] {1, 2, 3, 4};

            var result = source.Cast<int>().GetEnumerator().ToEnumerable();

            CollectionAssert.AreEqual(source, result.ToArray());
        }

        [TestMethod]
        public void Select()
        {
            var source = new[] {1, 2, 3, 4};
            var expected = new[] {2, 3, 4, 5};

            var result = source.GetEnumerator().Select<int, int>(x => x + 1);

            CollectionAssert.AreEqual(expected, result.ToArray());
        }

        [TestMethod]
        public void SelectGeneric()
        {
            var source = new[] {1, 2, 3, 4};
            var expected = new[] {2, 3, 4, 5};

            var result = source.Cast<int>().GetEnumerator().Select(x => x + 1);

            CollectionAssert.AreEqual(expected, result.ToArray());
        }
    }
}
