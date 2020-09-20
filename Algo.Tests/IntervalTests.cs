using System;
using System.Linq;
using Xunit;

namespace Algo.Tests
{
    public class IntervalTests
    {
        [Theory]
        [InlineData(0, 3,7,4,6,3,7)]
        [InlineData(0, 4,6,3,7,3,7)]
        [InlineData(0, 3,7,4,8,3,8)]
        [InlineData(0, 4,8,3,7,3,8)]
        [InlineData(0, 3,7,-2,4,-2,7)]
        [InlineData(0, 3,7,2,4,2,7)]
        [InlineData(0, 3,7,3,5,3,7)]
        [InlineData(0, 3,7,5,7,3,7)]
        [InlineData(0, 3,7,1,7,1,7)]
        [InlineData(0, 3,7,3,8,3,8)]
        [InlineData(5, 1,5,10,15,1,15)]
        [InlineData(5, 10,15,1,5,1,15)]
        [InlineData(5, -5, -1,-15,-10,-15,-1)]
        public void TryMerge_When_Can_Be_Merged_Then_Success(uint mergeDistance, int begin1, int end1, int begin2, int end2, int mergedBegin, int mergedEnd)
        {
            var interval1 = new Interval(begin1, end1);
            var interval2 = new Interval(begin2, end2);
            var isMerged1 = interval1.TryMerge(interval2, out var merged1, mergeDistance);
            var isMerged2 = interval2.TryMerge(interval1, out var merged2, mergeDistance);
            Assert.True(isMerged1);
            Assert.True(isMerged2);
            Assert.Equal(mergedBegin, merged1.Begin);
            Assert.Equal(mergedEnd, merged1.End);
            Assert.Equal(mergedBegin, merged2.Begin);
            Assert.Equal(mergedEnd, merged2.End);
        }

        [Theory]
        [InlineData(0, 3,7,-1,2)]
        [InlineData(0, 3,7,0,2)]
        [InlineData(0, 3,7,8,10)]
        [InlineData(5, 1,5,11,15)]
        [InlineData(5, 1,5,11,15)]
        [InlineData(5, -5,-1, -15, -11)]
        public void TryMerge_When_Can_Not_Be_Merged_Then_Fail(uint mergeDistance, int begin1, int end1, int begin2, int end2)
        {
            var interval1 = new Interval(begin1, end1);
            var interval2 = new Interval(begin2, end2);
            var isMerged1 = interval1.TryMerge(interval2, out var merged1, mergeDistance);
            var isMerged2 = interval2.TryMerge(interval1, out var merged2, mergeDistance);
            Assert.False(isMerged1);
            Assert.False(isMerged2);
            Assert.Null(merged1);
            Assert.Null(merged2);
        }

        [Fact]
        public void Constructor_When_End_Is_Less_Or_Equal_To_Begin_Then_Fail()
        {
            var begin = 5;
            var end = 1;
            Assert.Throws<ArgumentOutOfRangeException>(() => new Interval(begin, end));

            begin = 10;
            end = 10;
            Assert.Throws<ArgumentOutOfRangeException>(() => new Interval(begin, end));
        }

        [Theory]
        [InlineData(1,7,2,3,"[1,2],[3,7]")]
        [InlineData(1,10,1,5,"[5,10]")]
        [InlineData(1,10,2,6,"[1,2],[6,10]")]
        [InlineData(1,10,4,10,"[1,4]")]
        public void Split_When_Can_Be_Splitted_Then_Success(int begin1, int end1, int begin2, int end2, string expectedSplit)
        {
            var interval1 = new Interval(begin1, end1);
            var interval2 = new Interval(begin2, end2);
            var splitted = interval1.Split(interval2);
            var actual = string.Join(',',splitted.Select(x => $"[{x.Begin},{x.End}]"));
            Assert.Equal(expectedSplit, actual);
        }
    }
}
