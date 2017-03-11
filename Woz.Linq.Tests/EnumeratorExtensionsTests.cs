#region License
// This file is part of Woz.Linq.
// [https://github.com/WozSoftware/Woz.Linq]
//
// This is free and unencumbered software released into the public domain.
// 
// Anyone is free to copy, modify, publish, use, compile, sell, or
// distribute this software, either in source code form or as a compiled
// binary, for any purpose, commercial or non-commercial, and by any
// means.
// 
// In jurisdictions that recognize copyright laws, the author or authors
// of this software dedicate any and all copyright interest in the
// software to the public domain. We make this dedication for the benefit
// of the public at large and to the detriment of our heirs and
// successors. We intend this dedication to be an overt act of
// relinquishment in perpetuity of all present and future rights to this
// software under copyright law.
// 
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
// OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
// 
// For more information, please refer to<http://unlicense.org>
#endregion
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
