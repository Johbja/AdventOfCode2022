using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_Of_Code_2022.Extensions
{
    public static class LinqExtensions
    {
        public static IEnumerable<T> Flatten<T>(this IEnumerable<T> collection, Func<T, IEnumerable<T>> action) 
            => collection.SelectMany(item => action(item).Flatten(action)).Concat(collection);
    }
}
