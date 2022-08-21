namespace ZooLib.Tests.Utility
{
    public class FeedTimeTest
    {
        [Fact]
        public void ShouldBeAbleToCreateFeedTime()
        {
            var feedTime = new ZooLib.Utility.FeedTime(DateTime.Now, new Employees.ZooKeeper());

            Assert.NotNull(feedTime);
            Assert.NotNull(feedTime.ZooKeeper);
        }
    }
}
