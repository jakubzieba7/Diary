using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diary
{
    public static class ExceptHelper
    {
        public static IEnumerable<TSource> ExceptAll<TSource>(this IEnumerable<TSource> first,IEnumerable<TSource> second,IEqualityComparer<TSource> comparer = null)
        {
            if (first == null) { throw new ArgumentNullException(nameof(first)); }
            if (second == null) { throw new ArgumentNullException(nameof(second)); }


            var secondCounts = new Dictionary<TSource, int>(comparer ?? EqualityComparer<TSource>.Default);
            int count;
            int nullCount = 0;

            foreach (var item in second)
            {
                if (item == null)
                {
                    nullCount++;
                }
                else
                {
                    if (secondCounts.TryGetValue(item, out count))
                    {
                        secondCounts[item] = count + 1;
                    }
                    else
                    {
                        secondCounts.Add(item, 1);
                    }
                }
            }

            foreach (var item in first)
            {
                if (item == null)
                {
                    nullCount--;
                    if (nullCount < 0)
                    {
                        yield return item;
                    }
                }
                else
                {
                    if (secondCounts.TryGetValue(item, out count))
                    {
                        if (count == 0)
                        {
                            secondCounts.Remove(item);
                            yield return item;
                        }
                        else
                        {
                            secondCounts[item] = count - 1;
                        }
                    }
                    else
                    {
                        yield return item;
                    }
                }
            }
        }
    }
}
