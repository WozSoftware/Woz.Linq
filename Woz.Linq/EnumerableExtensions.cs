#region License
// Copyright (C) Woz.Software 2015
// [https://github.com/WozSoftware/Woz.Linq]
//
// This file is part of Woz.Linq.
//
// Woz.Linq is free software: you can redistribute it 
// and/or modify it under the terms of the GNU General Public 
// License as published by the Free Software Foundation, either 
// version 3 of the License, or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
#endregion

using System;
using System.Collections.Generic;
using System.Linq;

namespace Woz.Linq
{
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
        /// Prepends the value at the head of the enumerable
        /// </summary>
        /// <typeparam name="T">The value type</typeparam>
        /// <param name="tail">The list to prepend to</param>
        /// <param name="head">The value to prepend</param>
        /// <returns>The resulting list</returns>
        public static IEnumerable<T> Prepend<T>(this IEnumerable<T> tail, T head)
            => head.ToEnumerable().Concat(tail);

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
        public static T MinOrElse<T>(
            this IEnumerable<T> source, T orElseValue)
        {
            var buffer = source.ToArray();

            return buffer.Length > 0
                ? buffer.Min()
                : orElseValue;
        }

        /// <summary>
        /// Gets the max value of the list or returns the supplied value
        /// if the list is empty
        /// </summary>
        /// <typeparam name="T">The element type in the list</typeparam>
        /// <param name="source">The list to process</param>
        /// <param name="orElseValue">The default value if the list is empty</param>
        /// <returns>The max value</returns>
        public static T MaxOrElse<T>(
            this IEnumerable<T> source, T orElseValue)
        {
            var buffer = source.ToArray();

            return buffer.Length > 0
                ? buffer.Max()
                : orElseValue;
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
            this IEnumerable<T> source, 
            Func<T, TKey> selector, 
            Func<T> orElseFactory)
        {
            var buffer = source.ToArray();

            return buffer.Length > 0
                ? buffer.MinBy(selector)
                : orElseFactory();
        }

        /// <summary>
        /// Gets the element with the max selected value from the list
        /// </summary>
        /// <typeparam name="T">The element type</typeparam>
        /// <typeparam name="TKey">The value to test</typeparam>
        /// <param name="source">The element list</param>
        /// <param name="selector">Selector to apply to the element</param>
        /// <returns>The max element</returns>
        public static T MaxBy<T, TKey>(
            this IEnumerable<T> source, Func<T, TKey> selector)
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
            this IEnumerable<T> source, 
            Func<T, TKey> selector, 
            Func<T> orElseFactory)
        {
            var buffer = source.ToArray();

            return buffer.Length > 0
                ? buffer.MaxBy(selector)
                : orElseFactory();
        }

        private static T CompareBy<T, TKey>(
            this IEnumerable<T> source,
            Func<T, TKey> selector,
            Func<int, bool> isBetter)
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
        /// A lambda based version of ForEach
        /// </summary>
        /// <typeparam name="T">Element type of the list</typeparam>
        /// <param name="source">The list to process</param>
        /// <param name="action">The action to apply to the list</param>
        public static void Each<T>(
            this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
            {
                action(item);
            }
        }
    }
}