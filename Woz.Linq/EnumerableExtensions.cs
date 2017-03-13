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
using System;
using System.Collections.Generic;
using System.Linq;

namespace Woz.Linq
{
    /// <summary>
    /// A set of extensions that add functionality missing from Linq
    /// based around the IEnumerable interface
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Return the supplied value in an enumerable as the only element
        /// </summary>
        /// <typeparam name="T">The value type</typeparam>
        /// <param name="value">The value to wrap</param>
        /// <returns>The value wrapped in an enumerable</returns>
        public static IEnumerable<T> ToEnumerable<T>(this T value)
        {
            yield return value;
        }

        /// <summary>
        /// Concatinates the value to the end of the list
        /// </summary>
        /// <typeparam name="T">The value type</typeparam>
        /// <param name="head">The list to concatinate to</param>
        /// <param name="tail">The value to concatinate</param>
        /// <returns>The resulting list</returns>
        public static IEnumerable<T> Concat<T>(this IEnumerable<T> head, T tail)
            => head.Concat(tail.ToEnumerable());

        /// <summary>
        /// Gets the min value of the list or returns the supplied value
        /// if the list is empty
        /// </summary>
        /// <typeparam name="T">The element type in the list</typeparam>
        /// <param name="source">The list to process</param>
        /// <param name="orElseValue">The default value if the list is empty</param>
        /// <returns>The min value</returns>
        public static T MinOrElse<T>(this IEnumerable<T> source, T orElseValue)
        {
            var buffer = source.ToArray();
            return buffer.Length > 0 ? buffer.Min() : orElseValue;
        }

        /// <summary>
        /// Gets the max value of the list or returns the supplied value
        /// if the list is empty
        /// </summary>
        /// <typeparam name="T">The element type in the list</typeparam>
        /// <param name="source">The list to process</param>
        /// <param name="orElseValue">The default value if the list is empty</param>
        /// <returns>The max value</returns>
        public static T MaxOrElse<T>(this IEnumerable<T> source, T orElseValue)
        {
            var buffer = source.ToArray();
            return buffer.Length > 0 ? buffer.Max() : orElseValue;
        }

        /// <summary>
        /// Gets the element with the min selected value from the list
        /// </summary>
        /// <typeparam name="T">The element type</typeparam>
        /// <typeparam name="TKey">The value to test</typeparam>
        /// <param name="source">The element list</param>
        /// <param name="selector">Selector to apply to the element</param>
        /// <returns>The min element</returns>
        public static T MinBy<T, TKey>(
            this IEnumerable<T> source, Func<T, TKey> selector)
            => source.CompareBy(selector, x => x < 0);

        /// <summary>
        /// Gets the element with the min selected value from the list
        /// or calls the supplied factory if the list is empty
        /// </summary>
        /// <typeparam name="T">The element type</typeparam>
        /// <typeparam name="TKey">The value to test</typeparam>
        /// <param name="source">The element list</param>
        /// <param name="selector">Selector to apply to the element</param>
        /// <param name="orElseFactory">The factory to build the else value</param>
        /// <returns>The min element</returns>
        public static T MinByOrElse<T, TKey>(
            this IEnumerable<T> source, Func<T, TKey> selector, Func<T> orElseFactory)
        {
            var buffer = source.ToArray();
            return buffer.Length > 0 ? buffer.MinBy(selector) : orElseFactory();
        }

        /// <summary>
        /// Gets the element with the max selected value from the list
        /// </summary>
        /// <typeparam name="T">The element type</typeparam>
        /// <typeparam name="TKey">The value to test</typeparam>
        /// <param name="source">The element list</param>
        /// <param name="selector">Selector to apply to the element</param>
        /// <returns>The max element</returns>
        public static T MaxBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector)
            => source.CompareBy(selector, x => x > 0);

        /// <summary>
        /// Gets the element with the max selected value from the list
        /// or calls the supplied factory if the list is empty
        /// </summary>
        /// <typeparam name="T">The element type</typeparam>
        /// <typeparam name="TKey">The value to test</typeparam>
        /// <param name="source">The element list</param>
        /// <param name="selector">Selector to apply to the element</param>
        /// <param name="orElseFactory">The factory to build the else value</param>
        /// <returns>The max element</returns>
        public static T MaxByOrElse<T, TKey>(
            this IEnumerable<T> source, Func<T, TKey> selector, Func<T> orElseFactory)
        {
            var buffer = source.ToArray();
            return buffer.Length > 0 ? buffer.MaxBy(selector) : orElseFactory();
        }

        private static T CompareBy<T, TKey>(
            this IEnumerable<T> source, Func<T, TKey> selector, Func<int, bool> isBetter)
        {
            var comparer = Comparer<TKey>.Default;

            using (var enumerator = source.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                {
                    throw new InvalidOperationException("Sequence has no elements");
                }

                var best = enumerator.Current;
                var bestKey = selector(best);

                while (enumerator.MoveNext())
                {
                    var candidate = enumerator.Current;
                    var candidateKey = selector(candidate);

                    if (!isBetter(comparer.Compare(candidateKey, bestKey)))
                    {
                        continue;
                    }

                    best = candidate;
                    bestKey = candidateKey;
                }

                return best;
            }
        }

        /// <summary>
        /// Select the distinct object in the enumerable by a key
        /// </summary>
        /// <typeparam name="T">The element type</typeparam>
        /// <typeparam name="TKey">The key type</typeparam>
        /// <param name="source">The source list</param>
        /// <param name="keySelector">The key selector</param>
        /// <returns>The distinct list</returns>
        public static IEnumerable<T> DistinctBy<T, TKey>(
            this IEnumerable<T> source, Func<T, TKey> keySelector)
            => source.GroupBy(keySelector).Select(x => x.First());

        /// <summary>
        /// A lambda based version of ForEach
        /// </summary>
        /// <typeparam name="T">Element type of the list</typeparam>
        /// <param name="source">The list to process</param>
        /// <param name="action">The action to apply to the list</param>
        public static void ForEach<T>(
            this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
            {
                action(item);
            }
        }
    }
}