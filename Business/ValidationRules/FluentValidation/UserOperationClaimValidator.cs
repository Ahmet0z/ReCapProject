using Core.Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class UserOperationClaimValidator:AbstractValidator<UserOperationClaim>
    {
        public UserOperationClaimValidator()
        {
            RuleFor(uoc => uoc.UserId).NotEmpty();
            RuleFor(uoc => uoc.UserId).NotNull();
            RuleFor(uoc => uoc.UserId).NotEqual(0);
            RuleFor(uoc => uoc.OperationClaimId).NotNull();
            RuleFor(uoc => uoc.OperationClaimId).NotEmpty();
            RuleFor(uoc => uoc.OperationClaimId).NotEqual(0);
        }
    }
}
