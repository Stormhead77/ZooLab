using ZooLib.Animals.Birds;

namespace ZooLib.Tests.Animals.Birds
{
    public class BirdTest
    {
        [Theory]
        [MemberData(nameof(GenerateBirds))]
        public void ShouldBeAbleToCreateBird(Bird bird)
        {
            Assert.NotNull(bird);
        }

        private static IEnumerable<object[]> GenerateBirds()
        {
            yield return new object[] { new Parrot() };
            yield return new object[] { new Penguin() };
        }
    }
}
