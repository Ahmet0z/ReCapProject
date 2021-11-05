using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.DTOs;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class RegisterValidator:AbstractValidator<UserForRegisterDto>
    {
        public RegisterValidator()
        {
            RuleFor(u => u.Email).EmailAddress();
            RuleFor(u => u.Email).NotEmpty();
            RuleFor(u => u.FirstName).MinimumLength(2);
            RuleFor(u => u.FirstName).NotEmpty();
            RuleFor(u => u.LastName).MinimumLength(2);
            RuleFor(u => u.LastName).NotEmpty();
            RuleFor(u => u.Password).MinimumLength(8).WithMessage("Şifre en az 8 karakter olmalıdır!");
            RuleFor(u => u.Password).NotEmpty();

        }
    }
}
