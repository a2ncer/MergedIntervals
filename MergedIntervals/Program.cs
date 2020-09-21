using System;

namespace MergedIntervals
{
    class Program
    {
        const uint MERGE_DISTANCE = 7;
        const string SMALL_FILE_PATH = "Samples/input.txt";
        const string SMALL_FILE_PATH_WITH_DELETED = "Samples/input_deleted.txt";
        const string BIG_FILE_PATH = "Samples/1000_input.txt";
        const string THE_BIGGEST_FILE_PATH = "Samples/1_000_000_input.txt";
        const string BIG_FILE_PATH_WITH_DELETED = "Samples/1000_input_deleted.txt";

        static void Main(string[] args)
        {
            Console.WriteLine("Samples");
            Console.WriteLine("[1] IntervalCollectionAggregator 6 rows file.");
            Console.WriteLine("[2] IntervalCollectionAggregator 1000 rows file.");
            Console.WriteLine("[3] IntervalCollectionAggregator 1_000_000 rows file.");
            Console.WriteLine("[4] FileAggregator 6 rows file.");
            Console.WriteLine("[5] FileAggregator 1000 rows file.");
            Console.WriteLine("[6] FileAggregator with DELETED 8 rows file.");
            Console.WriteLine("[7] FileAggregator with DELETED 1000 rows file.");
            Console.WriteLine("[8] FileAggregator 1_000_000 rows file.");
            Console.WriteLine("Enter command number: ");
            
            if (!int.TryParse(Console.ReadLine(), out int input))
            {
                Console.WriteLine("Incorrect command");
                return;
            }

            switch(input)
            {
                case 1: new IntervalCollectionAggregator(MERGE_DISTANCE, SMALL_FILE_PATH).Process(); break;
                case 2: new IntervalCollectionAggregator(MERGE_DISTANCE, BIG_FILE_PATH).Process(); break;
                case 3: new IntervalCollectionAggregator(MERGE_DISTANCE, THE_BIGGEST_FILE_PATH).Process(); break;
                case 4: new FileAggregator(MERGE_DISTANCE, SMALL_FILE_PATH).Process(); break;
                case 5: new FileAggregator(MERGE_DISTANCE, BIG_FILE_PATH).Process(); break;
                case 6: new FileAggregator(MERGE_DISTANCE, SMALL_FILE_PATH_WITH_DELETED).Process();break;
                case 7: new FileAggregator(MERGE_DISTANCE, BIG_FILE_PATH_WITH_DELETED).Process(); break;
                case 8: new FileAggregator(MERGE_DISTANCE, THE_BIGGEST_FILE_PATH).Process(); break;
                default: Console.WriteLine("Command not found");break;
            }
        }
    }
}
