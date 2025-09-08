using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Application.Dtos.StudentDtos;

namespace Task2.Application.Validations
{
    public class StudentUpdateDtoValidation: AbstractValidator<StudentUpdateDto>
    {
        public StudentUpdateDtoValidation()
        {
            RuleFor(s => s.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Email Must Have @ sign");
            RuleFor(s => s.Name)
                .NotEmpty().MaximumLength(25)
                .MinimumLength(3)
                .WithMessage("Name Char between 3 to 25 char");
        }
    }
}
