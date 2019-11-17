using System.Linq;
using Xunit;

namespace Woz.Linq.Tests
{
    public class EnumeratorExtensionsTests
    {
        [Fact]
        public void ToArray()
        {
            var source = new[] {1, 2, 3, 4};

            var result = source.GetEnumerator().ToArray<int>();

            Assert.Equal(source, result);
        }

        [Fact]
        public void ToArrayGeneric()
        {
            var source = new[] {1, 2, 3, 4};

            var result = source.Cast<int>().GetEnumerator().ToArray();

            Assert.Equal(source, result);
        }

        [Fact]
        public void ToEnumerable()
        {
            var source = new[] {1, 2, 3, 4};

            var result = source.GetEnumerator().ToEnumerable<int>();

            Assert.Equal(source, result.ToArray());
        }

        [Fact]
        public void ToEnumerableGeneric()
        {
            var source = new[] {1, 2, 3, 4};

            var result = source.Cast<int>().GetEnumerator().ToEnumerable();

            Assert.Equal(source, result.ToArray());
        }

        [Fact]
        public void Select()
        {
            var source = new[] {1, 2, 3, 4};
            var expected = new[] {2, 3, 4, 5};

            var result = source.GetEnumerator().Select<int, int>(x => x + 1);

            Assert.Equal(expected, result.ToArray());
        }

        [Fact]
        public void SelectGeneric()
        {
            var source = new[] {1, 2, 3, 4};
            var expected = new[] {2, 3, 4, 5};

            var result = source.Cast<int>().GetEnumerator().Select(x => x + 1);

            Assert.Equal(expected, result.ToArray());
        }
    }
}
