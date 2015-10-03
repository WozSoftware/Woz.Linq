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

namespace Woz.Linq
{
    public static class EnumeratorExtensions
    {
        public static IEnumerable<T> ToEnumerable<T>(this IEnumerator<T> source)
        {
            return source.Select(x => x);
        }

        public static IEnumerable<TResult> Select<T, TResult>(
            this IEnumerator<T> source, Func<T, TResult> selector)
        {
            while (source.MoveNext())
            {
                yield return selector(source.Current);
            }
        }

    }
}