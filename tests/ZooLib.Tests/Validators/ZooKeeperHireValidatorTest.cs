using FluentValidation.TestHelper;
using ZooLib.Employees;
using ZooLib.Validators;

namespace ZooLib.Tests.Validators
{
    public class ZooKeeperHireValidatorTest
    {
        private readonly ZooKeeperHireValidator validator = new ZooKeeperHireValidator();

        [Fact]
        public void ShouldBeAbleToValidateEmployee()
        {
            ZooKeeper zooKeeper = new ZooKeeper();

            var result = validator.ValidateEmployee(zooKeeper);

            Assert.Contains(ZooKeeperHireValidator.EmptyFirstName, result);
            Assert.Contains(ZooKeeperHireValidator.EmptyLastName, result);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void ShouldReturnFirstNameRequired(string firstName)
        {
            ZooKeeper zooKeeper = new ZooKeeper { FirstName = firstName };

            var result = validator.TestValidate(zooKeeper);

            result.ShouldHaveValidationErrorFor(zooKeeper => zooKeeper.FirstName)
                .WithErrorMessage(ZooKeeperHireValidator.EmptyFirstName);
        }

        [Fact]
        public void ShouldReturnFirstNameTooLong()
        {
            ZooKeeper zooKeeper = new ZooKeeper { FirstName = new string('a', ZooKeeperHireValidator.FirstNameMaxLength + 1) };

            var result = validator.TestValidate(zooKeeper);

            result.ShouldHaveValidationErrorFor(zooKeeper => zooKeeper.FirstName)
                .WithErrorMessage(ZooKeeperHireValidator.LongFirstName);
        }

        [Theory]
        [MemberData(nameof(GenerateFirstNames))]
        public void ShouldNotReturnWrongFirstName(string firstName)
        {
            ZooKeeper zooKeeper = new ZooKeeper { FirstName = firstName };

            var result = validator.TestValidate(zooKeeper);

            result.ShouldNotHaveValidationErrorFor(zooKeeper => zooKeeper.FirstName);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void ShouldReturnLastNameRequired(string lastName)
        {
            ZooKeeper zooKeeper = new ZooKeeper { LastName = lastName };

            var result = validator.TestValidate(zooKeeper);

            result.ShouldHaveValidationErrorFor(zooKeeper => zooKeeper.LastName)
                .WithErrorMessage(ZooKeeperHireValidator.EmptyLastName);
        }

        [Fact]
        public void ShouldReturnLastNameTooLong()
        {
            ZooKeeper zooKeeper = new ZooKeeper { LastName = new string('a', ZooKeeperHireValidator.LastNameMaxLength + 1) };

            var result = validator.TestValidate(zooKeeper);

            result.ShouldHaveValidationErrorFor(zooKeeper => zooKeeper.LastName)
                .WithErrorMessage(ZooKeeperHireValidator.LongLastName);
        }

        [Theory]
        [MemberData(nameof(GenerateLastNames))]
        public void ShouldNotReturnWrongLastName(string lastName)
        {
            ZooKeeper zooKeeper = new ZooKeeper { LastName = lastName };

            var result = validator.TestValidate(zooKeeper);

            result.ShouldNotHaveValidationErrorFor(zooKeeper => zooKeeper.LastName);
        }

        private static IEnumerable<object[]> GenerateFirstNames()
        {
            yield return new object[] { "first" };
            yield return new object[] { new string('a', ZooKeeperHireValidator.FirstNameMaxLength) };
        }

        private static IEnumerable<object[]> GenerateLastNames()
        {
            yield return new object[] { "last" };
            yield return new object[] { new string('a', ZooKeeperHireValidator.LastNameMaxLength) };
        }
    }
}
