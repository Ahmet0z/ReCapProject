using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class CardValidator : AbstractValidator<Card>
    {
        public CardValidator()
        {
            RuleFor(c => c.CreditCardNumber).Length(16);
            RuleFor(c => c.ExpirationDate).Length(4);
            RuleFor(c => c.SecurityCode).Length(3);
            RuleFor(c => c.OwnerName).NotEmpty();
            RuleFor(c => c.UserId).NotEmpty();
        }
    }
}
