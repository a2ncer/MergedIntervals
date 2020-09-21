using System;
using System.IO;
using MergedIntervals.Core;

namespace MergedIntervals
{
    public class IntervalCollectionAggregator
    {
        private readonly uint _mergeDistance;
        private readonly string _pathSource;

        public IntervalCollectionAggregator(uint mergeDistance, string pathSource)
        {
            _mergeDistance = mergeDistance;
            _pathSource = pathSource;
        }

        public void Process()
        {
            using (var sr = new StreamReader(_pathSource))
            {
                String line = null;
                var collection = new IntervalCollection(_mergeDistance);
                while ((line = sr.ReadLine()) != null)
                {
                    var colums = line.Split(' ');
                    var intervalLine = new IntervalLine();
                    intervalLine.LineNumber = int.Parse(colums[0]);
                    intervalLine.Begin = int.Parse(colums[1]);
                    intervalLine.End = int.Parse(colums[2]);

                    switch (colums[3])
                    {
                        case "ADDED": intervalLine.Operation = LineOperation.ADDED; break;
                        case "REMOVED": intervalLine.Operation = LineOperation.REMOVED; break;
                        default: break;
                    }

                    switch (intervalLine.Operation)
                    {
                        case LineOperation.ADDED: collection.Add(intervalLine.Begin, intervalLine.End); break;
                        case LineOperation.REMOVED: collection.Remove(intervalLine.Begin, intervalLine.End); break;
                        default: break;
                    }

                    intervalLine.Output = collection;

                    Console.WriteLine(intervalLine);
                }
            }
        }
    }
}
