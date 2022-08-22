using ZooLib.Employees;
using ZooLib.Utility;

namespace ZooLib.Tests.Utility
{
    public class FeedTimeTest
    {
        [Fact]
        public void ShouldBeAbleToCreateFeedTime()
        {
            var feedTime = new FeedTime(DateTime.Now, new ZooKeeper());

            Assert.NotNull(feedTime);
            Assert.NotNull(feedTime.ZooKeeper);
        }
    }
}
