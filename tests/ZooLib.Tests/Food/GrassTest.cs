namespace ZooLib.Tests.Food
{
    public class GrassTest
    {
        [Fact]
        public void ShouldBeAbleToCreateGrass()
        {
            var grass = new ZooLib.Food.Grass();

            Assert.NotNull(grass);
        }
    }
}