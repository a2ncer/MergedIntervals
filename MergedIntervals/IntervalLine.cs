using System.Collections.Generic;
using System.Linq;
using MergedIntervals.Core;

namespace MergedIntervals
{
    public class IntervalLine
    {
        public int? LineNumber { get; set; }
        public int Begin { get; set; }
        public int End { get; set; }
        public LineOperation? Operation { get; set; }
        public IEnumerable<Interval> Output { get; set; }

        public override string ToString()
        {
            var outpuit = Output != null ? string.Join(' ', Output.Select(x => $"[{x.Begin},{x.End}]")) : string.Empty;
            return $"{LineNumber} {Begin} {End} {Operation} {outpuit}";
        }
    }

    public enum LineOperation
    {
        ADDED,
        REMOVED,
        DELETED
    }
}
