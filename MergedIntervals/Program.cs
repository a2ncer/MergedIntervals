using System;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using MergedIntervals.Core;

namespace MergedIntervals
{
    class Program
    {
        const uint MERGE_DISTANCE = 7;
        const string SMALL_FILE_PATH = "input.txt";
        const string SMALL_FILE_PATH_WITH_DELETED = "input_deleted.txt";
        const string BIG_FILE_PATH = "1000_input.txt";
        const string THE_BIGGEST_FILE_PATH = "1_000_000_input.txt";
        const string BIG_FILE_PATH_WITH_DELETED = "1000_input_deleted.txt";
        //1 1 20 ADDED [1, 20]
        //2 55 58 ADDED [1, 20][55, 58]
        //3 60 89 ADDED [1, 20][55, 89]
        //4 15 31 ADDED [1, 31][55, 89]
        //5 10 15 ADDED [1, 31][55, 89]
        //6 1 20 REMOVED [10, 31][55, 89]
        //7 10 15 DELETED [15, 31][55, 89]
        //8 16 40 ADDED [15, 40][55, 89]
        static void Main(string[] args)
        {
            //var ica = new IntervalCollectionAggregator(MERGE_DISTANCE, SMALL_FILE_PATH);
            //ica.Process();

            //var ica2 = new IntervalCollectionAggregator(MERGE_DISTANCE, BIG_FILE_PATH);
            //ica2.Process();

            var ica3 = new IntervalCollectionAggregator(MERGE_DISTANCE, THE_BIGGEST_FILE_PATH);
            ica3.Process();

            //var fa = new FileAggregator(MERGE_DISTANCE, BIG_FILE_PATH);
            //fa.Process();


            //var fa2 = new FileAggregator(MERGE_DISTANCE, SMALL_FILE_PATH_WITH_DELETED);
            //fa2.Process();

            //var fa3 = new FileAggregator(MERGE_DISTANCE, BIG_FILE_PATH_WITH_DELETED);
            //fa3.Process();

            //var fa4 = new FileAggregator(MERGE_DISTANCE, THE_BIGGEST_FILE_PATH);
            //fa4.Process();

            //var generator = new IntervalGenerator("1_000_000_input.txt", 1, 10000, 1000000, 25);
            //generator.Generate();
        }
    }
}
