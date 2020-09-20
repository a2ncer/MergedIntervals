using System;
using System.Collections.Generic;

namespace Algo
{
    public class Interval
    {
        public Interval(int begin, int end)
        {
            if (end <= begin)
            {
                throw new ArgumentOutOfRangeException("Begin value should be less then End value.");
            }
            Begin = begin;
            End = end;
        }

        public bool TryMerge(Interval other, out Interval merged, uint mergeDistance = 0)
        {
            merged = default;

            if (other.Begin >= Begin && other.Begin <= End + mergeDistance)
            {
                merged = new Interval(Begin, Math.Max(other.End, End));
                return true;
            }

            if (Begin >= other.Begin && Begin <= other.End + mergeDistance)
            {
                merged = new Interval(other.Begin, Math.Max(End, other.End));
                return true;
            }

            return false;
        }

        public IEnumerable<Interval> Split(Interval other)
        {
            if(other.Begin == Begin && other.End == End)
            {
                return new List<Interval>{new Interval(Begin, End)};
            }
            
            if(other.Begin == Begin && other.End < End)
            {
                return new List<Interval>{new Interval(other.End, End)};
            }

            if(other.Begin > Begin && other.End < End)
            {
                return new List<Interval>{new Interval(Begin, other.Begin), new Interval(other.End, End)};
            }

            if(other.Begin > Begin && other.End == End)
            {
                return new List<Interval>{new Interval(Begin, other.Begin)};
            }

            return default;
        }

        public int Begin { get; }
        public int End { get; }
    }
}