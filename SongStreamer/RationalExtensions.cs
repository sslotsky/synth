using Rationals;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SongStreamer
{
    public static class RationalExtensions
    {
        public static Rational Sum<T>(this IEnumerable<T> collection, Func<T, Rational> predicate)
        {
            if (collection.Count() == 0)
                return 0;

            return predicate(collection.First()) + collection.Skip(1).Sum(predicate);
        }
    }
}
