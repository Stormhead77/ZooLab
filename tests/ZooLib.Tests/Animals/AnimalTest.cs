using ZooLib.Animals;
using ZooLib.Animals.Birds;
using ZooLib.Animals.Mammals;
using ZooLib.Animals.Reptiles;

namespace ZooLib.Tests.Animals
{
    public class AnimalTest
    {
        [Theory]
        [MemberData(nameof(GenerateAnimals))]
        public void ShouldBeAbleToCreateAnimal(Animal animal)
        {
            Assert.NotNull(animal);
        }

        private static IEnumerable<object[]> GenerateAnimals()
        {
            yield return new object[] { new Bison() };
            yield return new object[] { new Elephant() };
            yield return new object[] { new Lion() };
            yield return new object[] { new Parrot() };
            yield return new object[] { new Penguin() };
            yield return new object[] { new Snake() };
            yield return new object[] { new Turtle() };
        }
    }
}
