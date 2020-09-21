using System;
using System.Collections.Generic;
using System.Linq;

namespace MergedIntervals.Core.Extensions
{
    public static class IntervalCollectionExtensions
    {
        public static IEnumerable<Interval> Delete(this IntervalCollection collection, int begin, int end)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            
            var merged = collection.ToList();
            var index = merged.FindIndex(x => begin >= x.Begin && end <= x.End);
            if (index == -1)
            {
                return collection;
            }
            var current = merged[index];
            var split = current.Split(new Interval(begin, end));

            if (split == default)
            {
                return collection;
            }

            merged.InsertRange(index, split);
            merged.RemoveAt(index + split.Count());

            return merged;
        }

        public static IntervalCollection AddRange(this IntervalCollection collection, IEnumerable<Interval> range)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (range is null)
            {
                throw new ArgumentNullException(nameof(range));
            }

            foreach (var interval in range)
            {
                collection.Add(interval.Begin, interval.End);
            }

            return collection;
        }
    }
}