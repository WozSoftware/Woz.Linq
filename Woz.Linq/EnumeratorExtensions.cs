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
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Woz.Linq
{
    public static class EnumeratorExtensions
    {
        /// <summary>
        /// Unpack the enumerator and return its contents in an array
        /// </summary>
        /// <typeparam name="T">The type in the enumerator</typeparam>
        /// <param name="source">The enumerator</param>
        /// <returns>The contents of the enumerator in an array</returns>
        public static T[] ToArray<T>(this IEnumerator source)
            => source.ToEnumerable<T>().ToArray();

        /// <summary>
        /// Unpack the enumerator and return its contents in an array
        /// </summary>
        /// <typeparam name="T">The type in the enumerator</typeparam>
        /// <param name="source">The enumerator</param>
        /// <returns>The contents of the enumerator in an array</returns>
        public static T[] ToArray<T>(this IEnumerator<T> source)
            => source.ToEnumerable().ToArray();

        /// <summary>
        /// Unpack the enumerator and return its contents as a simgle 
        /// shot enumerable
        /// </summary>
        /// <typeparam name="T">The type in the enumerator</typeparam>
        /// <param name="source">The enumerator</param>
        /// <returns>The contents of the enumerator as an enumerable</returns>
        public static IEnumerable<T> ToEnumerable<T>(this IEnumerator source)
            => source.Select<T, T>(Identity);

        /// <summary>
        /// Unpack the enumerator and return its contents as a simgle 
        /// shot enumerable
        /// </summary>
        /// <typeparam name="T">The type in the enumerator</typeparam>
        /// <param name="source">The enumerator</param>
        /// <returns>The contents of the enumerator as an enumerable</returns>
        public static IEnumerable<T> ToEnumerable<T>(this IEnumerator<T> source)
            => source.Select(Identity);

        /// <summary>
        /// Unpack the enumerator run a selector over the elements and 
        /// return the selected data as a simgle shot enumerable
        /// </summary>
        /// <typeparam name="T">The type in the enumerator</typeparam>
        /// <typeparam name="TResult">The type in the selected data</typeparam>
        /// <param name="source">The enumerator</param>
        /// <param name="selector">The selector for the enumerable items</param>
        /// <returns>The contents of the enumerator as an enumerable</returns>
        public static IEnumerable<TResult> Select<T, TResult>(
            this IEnumerator source, Func<T, TResult> selector)
        {
            while (source.MoveNext())
            {
                yield return selector((T)source.Current);
            }
        }

        /// <summary>
        /// Unpack the enumerator run a selector over the elements and 
        /// return the selected data as a simgle shot enumerable
        /// </summary>
        /// <typeparam name="T">The type in the enumerator</typeparam>
        /// <typeparam name="TResult">The type in the selected data</typeparam>
        /// <param name="source">The enumerator</param>
        /// <param name="selector">The selector for the enumerable items</param>
        /// <returns>The contents of the enumerator as an enumerable</returns>
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