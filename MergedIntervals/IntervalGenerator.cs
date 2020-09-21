using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MergedIntervals
{
    public class IntervalGenerator
    {
        private readonly string _path;
        private readonly int _begin;
        private readonly int _end;
        private readonly int _count;
        private readonly int _maxWindowSize;
        private readonly Queue<IntervalLine> _list;

        public IntervalGenerator(string path, int begin, int end, int count, int maxWindowSize)
        {
            _path = path;
            _begin = begin;
            _end = end;
            _count = count;
            _maxWindowSize = maxWindowSize;
            _list = new Queue<IntervalLine>();
        }

        public void Generate()
        {
            _list.Clear();
            using(var f = File.CreateText(_path))
            {
                for(int i=1; i<= _count; i++)
                {
                    var line = new IntervalLine();
                    line.LineNumber = i;
                    var size = new Random().Next(1, _maxWindowSize);
                    //var intervalEnd = new Random().Next(_begin + size, _end + size);
                    var intervalEnd = i + size;
                    var chance = new Random().NextDouble();
                    line.End = intervalEnd;
                    line.Begin = intervalEnd - size;
                    //if (chance > 0.99)
                    //{
                    //    line = _list.Any() ? _list.Dequeue() : line;

                    //    var newBegin = line.Begin;
                    //    var newEnd = (line.Begin + line.End) / 2;

                    //    if (newBegin < newEnd)
                    //    {
                    //        line.Begin = newBegin;
                    //        line.End = newEnd;
                    //    }
                    //    line.LineNumber = i;
                    //    line.Operation = LineOperation.DELETED;
                    //}
                    //else
                    if (chance > 0.98 && chance < 0.99)
                    {
                        line = _list.Any() ? _list.Dequeue() : line;
                        line.LineNumber = i;
                        line.Operation = LineOperation.REMOVED;
                    }
                    else
                    {
                        line.Operation = LineOperation.ADDED;
                        _list.Enqueue(line);
                    }

                    f.WriteLine(line.ToString().Trim());
                }
            }
        }
    }
}
