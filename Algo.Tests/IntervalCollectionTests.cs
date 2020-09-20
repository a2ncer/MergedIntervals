using System.Linq;
using System.Text.RegularExpressions;
using Algo.Intervals;
using Algo.Intervals.Extensions;
using Xunit;

namespace Algo.Tests
{
    public class IntervalCollectionTests
    {
        [Theory]
        [InlineData(0, "[1,2],[2,3]","[1,3]")]
        [InlineData(0, "[2,3],[1,2]","[1,3]")]
        [InlineData(0, "[1,2],[2,3],[4,5]","[1,3],[4,5]")]
        [InlineData(0, "[-7,-5],[-9,-6],[-11,-8]","[-11,-5]")]
        [InlineData(5, "[1,5],[10,15]","[1,15]")]
        [InlineData(5, "[10,15],[1,5]","[1,15]")]
        [InlineData(5, "[1,5],[11,15]","[1,5],[11,15]")]
        [InlineData(7, "[1,20]","[1,20]")]
        [InlineData(7, "[1,20],[55,58]","[1,20],[55,58]")]
        [InlineData(7, "[1,20],[55,58],[60,89]","[1,20],[55,89]")]
        [InlineData(7, "[1,20],[55,58],[60,89],[15,31]","[1,31],[55,89]")]
        [InlineData(7, "[1,20],[55,58],[60,89],[15,31],[10,15]","[1,31],[55,89]")]
        [InlineData(0, "","")]
        public void Given_IntervalCollection_When_Correct_Intervals_Then_Success(uint mergeDistance, string intervalsString, string expectedIntervalsString)
        {
            var regex = new Regex(@"\[(\-*\d+),(\-*\d+)\]");
            var matchCollection = regex.Matches(intervalsString);

            var intervalCollection = new IntervalCollection(mergeDistance);

            foreach(Match match in matchCollection)
            {
                intervalCollection.Add(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value));
            }

            var list = intervalCollection.ToList();

            var actual = string.Join(',',intervalCollection.Select(x => $"[{x.Begin},{x.End}]"));
            Assert.Equal(expectedIntervalsString, actual);
        }

        [Theory]
        [InlineData("[1,6],[5,7]",2,3,"[1,2],[3,7]")]
        [InlineData("[1,4],[2,10]",5,10,"[1,5]")]
        public void Delete_When_Correct_Then_Success(string intervalsString, int begin, int end, string expectedIntervalsString)
        {
            var regex = new Regex(@"\[(\-*\d+),(\-*\d+)\]");
            var matchCollection = regex.Matches(intervalsString);

            var intervalCollection = new IntervalCollection();

            foreach(Match match in matchCollection)
            {
                intervalCollection.Add(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value));
            }

            var actualList = intervalCollection.Delete(begin, end).ToList();

            var actual = string.Join(',',actualList.Select(x => $"[{x.Begin},{x.End}]"));
            Assert.Equal(expectedIntervalsString, actual);
        }
    }
}