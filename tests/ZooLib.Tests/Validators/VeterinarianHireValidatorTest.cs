using FluentValidation.TestHelper;
using ZooLib.Employees;
using ZooLib.Validators;

namespace ZooLib.Tests.Validators
{
    public class VeterinarianHireValidatorTest
    {
        private readonly VeterinarianHireValidator validator = new VeterinarianHireValidator();

        [Fact]
        public void ShouldBeAbleToValidateEmployee()
        {
            Veterinarian veterinarian = new Veterinarian();

            var result = validator.ValidateEmployee(veterinarian);

            Assert.Contains(VeterinarianHireValidator.EmptyFirstName, result);
            Assert.Contains(VeterinarianHireValidator.EmptyLastName, result);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void ShouldReturnFirstNameRequired(string firstName)
        {
            Veterinarian veterinarian = new Veterinarian { FirstName = firstName };

            var result = validator.TestValidate(veterinarian);

            result.ShouldHaveValidationErrorFor(veterinarian => veterinarian.FirstName)
                .WithErrorMessage(VeterinarianHireValidator.EmptyFirstName);
        }

        [Fact]
        public void ShouldReturnFirstNameTooLong()
        {
            Veterinarian veterinarian = new Veterinarian { FirstName = new string('a', VeterinarianHireValidator.FirstNameMaxLength + 1) };

            var result = validator.TestValidate(veterinarian);

            result.ShouldHaveValidationErrorFor(veterinarian => veterinarian.FirstName)
                .WithErrorMessage(VeterinarianHireValidator.LongFirstName);
        }

        [Theory]
        [MemberData(nameof(GenerateFirstNames))]
        public void ShouldNotReturnWrongFirstName(string firstName)
        {
            Veterinarian veterinarian = new Veterinarian { FirstName = firstName };

            var result = validator.TestValidate(veterinarian);

            result.ShouldNotHaveValidationErrorFor(veterinarian => veterinarian.FirstName);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void ShouldReturnLastNameRequired(string lastName)
        {
            Veterinarian veterinarian = new Veterinarian { LastName = lastName };

            var result = validator.TestValidate(veterinarian);

            result.ShouldHaveValidationErrorFor(veterinarian => veterinarian.LastName)
                .WithErrorMessage(VeterinarianHireValidator.EmptyLastName);
        }

        [Fact]
        public void ShouldReturnLastNameTooLong()
        {
            Veterinarian veterinarian = new Veterinarian { LastName = new string('a', VeterinarianHireValidator.LastNameMaxLength + 1) };

            var result = validator.TestValidate(veterinarian);

            result.ShouldHaveValidationErrorFor(veterinarian => veterinarian.LastName)
                .WithErrorMessage(VeterinarianHireValidator.LongLastName);
        }

        [Theory]
        [MemberData(nameof(GenerateLastNames))]
        public void ShouldNotReturnWrongLastName(string lastName)
        {
            Veterinarian veterinarian = new Veterinarian { LastName = lastName };

            var result = validator.TestValidate(veterinarian);

            result.ShouldNotHaveValidationErrorFor(veterinarian => veterinarian.LastName);
        }

        private static IEnumerable<object[]> GenerateFirstNames()
        {
            yield return new object[] { "first" };
            yield return new object[] { new string('a', VeterinarianHireValidator.FirstNameMaxLength) };
        }

        private static IEnumerable<object[]> GenerateLastNames()
        {
            yield return new object[] { "last" };
            yield return new object[] { new string('a', VeterinarianHireValidator.LastNameMaxLength) };
        }
    }
}
