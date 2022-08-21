namespace ZooLib.Tests.Food
{
    public class MeatTest
    {
        [Fact]
        public void ShouldBeAbleToCreateMeat()
        {
            var meat = new ZooLib.Food.Meat();

            Assert.NotNull(meat);
        }
    }
}