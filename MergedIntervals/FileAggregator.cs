using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using MergedIntervals.Core;
using MergedIntervals.Core.Extensions;

namespace MergedIntervals
{
    public class FileAggregator
    {
        private readonly uint _mergeDistance;
        private readonly string _pathSource;
        private readonly bool _consoleOutput;
        private readonly string _tmpSource;
        private readonly string _pathOutput;
        private readonly Regex _regex;

        public FileAggregator(uint mergeDistance, string pathSource, bool consoleOutput = true, string pathOutput = null)
        {
            _mergeDistance = mergeDistance;
            _pathSource = pathSource;
            _consoleOutput = consoleOutput;
            _pathOutput = pathOutput;
            _tmpSource = $"{Guid.NewGuid()}.txt";
            _regex = new Regex(@"\[(\-*\d+),(\-*\d+)\]");
        }

        public void Process()
        {
            ProcessInterval(null, null);
        }

        private void WriteIntervalLine(IntervalLine line)
        {
            if (_consoleOutput)
            {
                Console.WriteLine(line);
            }

            if (_pathOutput == null || line == null)
            {
                return;
            }
            
            using (var writer = File.AppendText(_pathOutput))
            {
                writer.WriteLine(line);
            }
        }

        private IEnumerable<Interval> ParseIntervals(string intervals)
        {
            if (intervals == null)
            {
                return default;
            }
            
            var matchCollection = _regex.Matches(intervals);
            return matchCollection.Select(x => new Interval(int.Parse(x.Groups[1].Value), int.Parse(x.Groups[2].Value)));
        }

        private IntervalLine ParseLine(string line)
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
                case "DELETED": intervalLine.Operation = LineOperation.DELETED; break;
                default: break;
            }

            if (colums.Length > 4)
            {
                intervalLine.Output = ParseIntervals(line);
            }

            return intervalLine;
        }

        private void ProcessInterval(int? begin, int? end)
        {
            using (var sr = new StreamReader(_pathSource))
            {
                String line = null;
                IEnumerable<Interval> previous = new List<Interval>();
                bool intervalDetected = false;
                
                while ((line = sr.ReadLine()) != null)
                {
                    var intervalLine = ParseLine(line);

                    if (begin.HasValue && end.HasValue && intervalLine.Begin == begin.Value && intervalLine.End == end.Value && intervalLine.Operation == LineOperation.ADDED)
                    {
                        intervalDetected = true;
                        continue;
                    }

                    if (intervalLine.Output != null && !intervalDetected)
                    {
                        previous = intervalLine.Output;
                    }

                    var collection = new IntervalCollection(_mergeDistance).AddRange(previous);
                    
                    switch (intervalLine.Operation)
                    {
                        case LineOperation.ADDED: 
                            collection.Add(intervalLine.Begin, intervalLine.End); 
                            previous = collection.ToList();
                            intervalLine.Output = previous;
                            StoreInterval(intervalLine); 
                            break;
                        case LineOperation.REMOVED:
                            var newIntervals = RemoveInterval(intervalLine.Begin, intervalLine.End);
                            previous = newIntervals ?? collection;
                            intervalLine.Output = previous;
                            break;
                        case LineOperation.DELETED: 
                            previous = collection.Delete(intervalLine.Begin, intervalLine.End);
                            intervalLine.Output = previous;
                            StoreInterval(intervalLine);
                            break;
                        default: break;
                    }

                    WriteIntervalLine(intervalLine);
                }
            }
            ClearFiles();
        }

        private void ClearFiles()
        {
            if (File.Exists(_tmpSource))
            {
                File.Delete(_tmpSource);
            }
        }

        private IEnumerable<Interval> RemoveInterval(int begin, int end)
        {
            var tmp = $"{Guid.NewGuid()}_remove_interval.txt";
            
            var bigFile = new FileAggregator(_mergeDistance, _tmpSource, false, tmp);

            bigFile.ProcessInterval(begin, end);

            string lastLine = File.ReadLines(tmp).LastOrDefault();

            var intervalLine = ParseLine(lastLine);

            File.Delete(tmp);

            return intervalLine.Output;
        }

        private void StoreInterval(IntervalLine intervalLine)
        {
            using (var sw = File.AppendText(_tmpSource))
            {
                var line = intervalLine.ToString().Trim();
                sw.WriteLine(line);
            }
        }
    }
}
