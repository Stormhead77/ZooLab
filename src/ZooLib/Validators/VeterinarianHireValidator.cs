﻿using FluentValidation;
using ZooLib.Employees;

namespace ZooLib.Validators
{
    public class VeterinarianHireValidator : AbstractValidator<Veterinarian>, IHireValidator
    {
        public const int FirstNameMaxLength = 50;
        public const int LastNameMaxLength = 50;
        public const string EmptyFirstName = "First Name required";
        public const string LongFirstName = "First Name too long";
        public const string EmptyLastName = "Last Name required";
        public const string LongLastName = "Last Name too long";

        public VeterinarianHireValidator()
        {
            RuleFor(veterinarian => veterinarian.FirstName)
                .NotEmpty().WithMessage(EmptyFirstName)
                .MaximumLength(FirstNameMaxLength).WithMessage(LongFirstName);
            RuleFor(veterinarian => veterinarian.LastName)
                .NotEmpty().WithMessage(EmptyLastName)
                .MaximumLength(LastNameMaxLength).WithMessage(LongLastName);
        }

        public List<string> ValidateEmployee(IEmployee employee)
        {
            var errors = Validate((Veterinarian)employee).Errors;

            var result = errors.Select(error => error.ErrorMessage).ToList();

            return result;
        }
    }
}
