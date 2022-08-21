namespace ZooLib.Tests.Medicine
{
    public class AntiDepressionTest
    {
        [Fact]
        public void ShouldBeAbleToCreateAntiDepression()
        {
            var antiDepression = new ZooLib.Medicine.AntiDepression();

            Assert.NotNull(antiDepression);
        }
    }
}