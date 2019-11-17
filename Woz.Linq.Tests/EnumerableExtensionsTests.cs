using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Woz.Linq.Tests
{
    public class EnumerableExtensionsTests
    {
        private class TestObject
        {
            public int Value { get; set; }
        }

        [Fact]
        public void ToEnumerable()
        {
            Assert.Equal(new[] {1}, 1.ToEnumerable().ToArray());
        }

        [Fact]
        public void Concat()
        {
            var source = new[] {2, 3, 4};
            var expected = new[] {2, 3, 4, 5};

            Assert.Equal(expected, source.Concat(5).ToArray());
        }

        [Fact]
        public void MinOrElseWhenValues()
        {
            Assert.Equal(2, new[] {3, 2, 5}.MinOrElse(6));
        }

        [Fact]
        public void MinOrElseNoValues()
        {
            Assert.Equal(2, new int[0].MinOrElse(2));
        }

        [Fact]
        public void MaxOrElseWhenValues()
        {
            Assert.Equal(5, new[] {3, 2, 5}.MaxOrElse(6));
        }

        [Fact]
        public void MaxOrElseNoValues()
        {
            Assert.Equal(2, new int[0].MaxOrElse(2));
        }

        [Fact]
        public void MinBy()
        {
            var source =
                new[]
                {
                    new TestObject {Value = 3},
                    new TestObject {Value = 2},
                    new TestObject {Value = 5}
                };

            Assert.Same(source[1], source.MinBy(x => x.Value));
        }

        [Fact]
        public void MinByOrElseWhenValues()
        {
            var source =
                new[]
                {
                    new TestObject {Value = 3},
                    new TestObject {Value = 2},
                    new TestObject {Value = 5}
                };

            Assert.Same(
                source[1],
                source.MinByOrElse(x => x.Value, () => null));
        }

        [Fact]
        public void MinByOrElseNoValues()
        {
            var source = new TestObject[0];
            var expected = new TestObject {Value = 3};

            Assert.Same(
                expected,
                source.MinByOrElse(x => x.Value, () => expected));
        }

        [Fact]
        public void MaxByOrElseWhenValues()
        {
            var source =
                new[]
                {
                    new TestObject {Value = 3},
                    new TestObject {Value = 2},
                    new TestObject {Value = 5}
                };

            Assert.Same(
                source[2],
                source.MaxByOrElse(x => x.Value, () => null));
        }

        [Fact]
        public void MaxByOrElseNoValues()
        {
            var source = new TestObject[0];
            var expected = new TestObject {Value = 3};

            Assert.Same(
                expected,
                source.MaxByOrElse(x => x.Value, () => expected));
        }

        [Fact]
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

            Assert.Equal(
                new[] {1, 2}, 
                source.DistinctBy(x => x.Key).Select(x => x.Key).ToArray());
        }


        [Fact]
        public void ForEach()
        {
            var source = new[] {1, 2, 3};
            var resultBuffer = new List<int>();

            source.ForEach(resultBuffer.Add);

            Assert.Equal(source, resultBuffer);
        }
    }
}