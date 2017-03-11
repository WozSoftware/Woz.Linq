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
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Woz.Linq
{
    /// <summary>
    /// A set of extensions that add functionality missing from Linq
    /// based around the IEnumerator interface
    /// </summary>
    public static class EnumeratorExtensions
    {
        // TODO: Split into partial classes when grows too big

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