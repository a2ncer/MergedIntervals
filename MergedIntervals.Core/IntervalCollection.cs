using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace MergedIntervals.Core
{
    public class IntervalCollection : IEnumerable<Interval>
    {
        private readonly SortedList<int, SortedList<int, Interval>> _list;
        private readonly uint _mergeDistance;

        public IntervalCollection(uint mergeDistance = 0)
        {
            _list = new SortedList<int, SortedList<int, Interval>>();
            _mergeDistance = mergeDistance;
        }

        public void Add(int begin, int end)
        {
            var interval = new Interval(begin, end);
            if (_list.ContainsKey(begin))
            {
                if (!_list[begin].ContainsKey(end))
                {
                    _list[begin].Add(end, interval);
                }
            }
            else
            {
                _list.Add(begin, new SortedList<int, Interval> { { end, interval } });
            }
        }

        public bool Remove(int begin, int end)
        {
            if (_list.ContainsKey(begin) && _list[begin].ContainsKey(end))
            {
                if (_list[begin].Count() > 1)
                {
                    _list[begin].Remove(end);
                }
                else
                {
                    _list.Remove(begin);
                }
            }

            return false;
        }

        public IEnumerator<Interval> GetEnumerator()
        {
            var sortedIntervals = _list.Select(x => x.Value.Last().Value);
            var merged = new List<Interval>();
            foreach (var interval in sortedIntervals)
            {
                if (merged.Any() && merged.Last().TryMerge(interval, out var newInterval, _mergeDistance))
                {
                    merged[merged.Count - 1] = newInterval;
                    continue;
                }
                merged.Add(interval);
            }
            return merged.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}