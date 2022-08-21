namespace ZooLib.Tests.Food
{
    public class FoodTest
    {
        [Theory]
        [MemberData(nameof(GenerateFood))]
        public void ShouldBeAbleToCreateFood(ZooLib.Food.Food food)
        {
            Assert.NotNull(food);
        }

        private static IEnumerable<object[]> GenerateFood()
        {
            yield return new object[] { new ZooLib.Food.Grass() };
            yield return new object[] { new ZooLib.Food.Meat() };
            yield return new object[] { new ZooLib.Food.Vegetables() };
        }
    }
}