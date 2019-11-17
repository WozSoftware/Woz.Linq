using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Woz.Linq
{
    public static class EnumeratorExtensions
    {
        public static T[] ToArray<T>(this IEnumerator source)
            => source.ToEnumerable<T>().ToArray();

        public static T[] ToArray<T>(this IEnumerator<T> source)
            => source.ToEnumerable().ToArray();

        public static IEnumerable<T> ToEnumerable<T>(this IEnumerator source)
            => source.Select<T, T>(Identity);

        public static IEnumerable<T> ToEnumerable<T>(this IEnumerator<T> source)
            => source.Select(Identity);

        public static IEnumerable<TResult> Select<T, TResult>(
            this IEnumerator source, Func<T, TResult> selector)
        {
            while (source.MoveNext())
            {
#pragma warning disable CS8601 // Possible null reference assignment.
                // Can't be null as valid move next completed
                yield return selector((T)source.Current);
#pragma warning restore CS8601 // Possible null reference assignment.
            }
        }

        public static IEnumerable<TResult> Select<T, TResult>(
            this IEnumerator<T> source, Func<T, TResult> selector)
        {
            while (source.MoveNext())
            {
                yield return selector(source.Current);
            }
        }

        private static T Identity<T>(T value) => value;
    }
}