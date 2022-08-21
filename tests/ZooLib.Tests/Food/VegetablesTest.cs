namespace ZooLib.Tests.Food
{
    public class VegetablesTest
    {
        [Fact]
        public void ShouldBeAbleToCreateVegetables()
        {
            var vegetables = new ZooLib.Food.Vegetables();

            Assert.NotNull(vegetables);
        }
    }
}