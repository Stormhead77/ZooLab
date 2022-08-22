using ZooLib.Validators;

namespace ZooLib.Tests.Validators
{
    public class IHireValidatorTest
    {
        [Theory]
        [MemberData(nameof(GenerateHireValidators))]
        public void ShouldBeAbleToCreateHireValidator(IHireValidator hireValidator)
        {
            Assert.NotNull(hireValidator);
        }

        private static IEnumerable<object[]> GenerateHireValidators()
        {
            yield return new object[] { new ZooKeeperHireValidator() };
            yield return new object[] { new VeterinarianHireValidator() };
        }
    }
}
