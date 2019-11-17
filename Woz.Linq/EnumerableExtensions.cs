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

        public static IEnumerable<T> Concat<T>(this IEnumerable<T> head, T tail)
            => head.Concat(tail.ToEnumerable());

        public static T MinOrElse<T>(this IEnumerable<T> source, T orElseValue)
        {
            var buffer = source.ToArray();
            return buffer.Length > 0 ? buffer.Min() : orElseValue;
        }

        public static T MaxOrElse<T>(this IEnumerable<T> source, T orElseValue)
        {
            var buffer = source.ToArray();
            return buffer.Length > 0 ? buffer.Max() : orElseValue;
        }

        public static T MinBy<T, TKey>(
            this IEnumerable<T> source, Func<T, TKey> selector)
            => source.CompareBy(selector, x => x < 0);

        public static T MinByOrElse<T, TKey>(
            this IEnumerable<T> source, Func<T, TKey> selector, Func<T> orElseFactory)
        {
            var buffer = source.ToArray();
            return buffer.Length > 0 ? buffer.MinBy(selector) : orElseFactory();
        }

        public static T MaxBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector)
            => source.CompareBy(selector, x => x > 0);

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

        public static IEnumerable<T> DistinctBy<T, TKey>(
            this IEnumerable<T> source, Func<T, TKey> keySelector)
            => source.GroupBy(keySelector).Select(x => x.First());

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