namespace ZooLib.Tests.Medicine
{
    public class AntiInflammatoryTest
    {
        [Fact]
        public void ShouldBeAbleToCreateAntiInflammatory()
        {
            var antiInflammatory = new ZooLib.Medicine.AntiInflammatory();

            Assert.NotNull(antiInflammatory);
        }
    }
}