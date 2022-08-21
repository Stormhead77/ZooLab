namespace ZooLib.Tests.Medicine
{
    public class AntibioticsTest
    {
        [Fact]
        public void ShouldBeAbleToCreateAntibiotics()
        {
            var antibiotics = new ZooLib.Medicine.Antibiotics();

            Assert.NotNull(antibiotics);
        }
    }
}