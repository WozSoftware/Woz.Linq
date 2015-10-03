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
        public static IEnumerable<T> ToEnumerable<T>(this T value)
        {
            yield return value;
        }

        public static IEnumerable<T> Concat<T>(this T head, IEnumerable<T> tail)
        {
            return head.ToEnumerable().Concat(tail);
        } 

        public static IEnumerable<T> Concat<T>(this IEnumerable<T> head, T tail)
        {
            return head.Concat(tail.ToEnumerable());
        } 

        public static T MinOrElse<T>(
            this IEnumerable<T> source, T orElseValue)
        {
            var buffer = source.ToArray();

            return buffer.Length > 0
                ? buffer.Min()
                : orElseValue;
        }

        public static T MaxOrElse<T>(
            this IEnumerable<T> source, T orElseValue)
        {
            var buffer = source.ToArray();

            return buffer.Length > 0
                ? buffer.Max()
                : orElseValue;
        }

        public static T MinBy<T, TKey>(
            this IEnumerable<T> source, Func<T, TKey> selector)
        {
            return source.CompareBy(selector, x => x < 0);
        }

        public static T MinByOrElse<T, TKey>(
            this IEnumerable<T> source, Func<T, TKey> selector, T orElseValue)
        {
            var buffer = source.ToArray();

            return buffer.Length > 0
                ? buffer.MinBy(selector)
                : orElseValue;
        }

        public static T MaxBy<T, TKey>(
            this IEnumerable<T> source, Func<T, TKey> selector)
        {
            return source.CompareBy(selector, x => x > 0);
        }

        public static T MaxByOrElse<T, TKey>(
            this IEnumerable<T> source, Func<T, TKey> selector, T orElseValue)
        {
            var buffer = source.ToArray();

            return buffer.Length > 0
                ? buffer.MaxBy(selector)
                : orElseValue;
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