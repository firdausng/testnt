using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Testnt.Main.Application.Common
{
    public abstract class BaseTenantValidator<T>: AbstractValidator<T> where T:BaseRequest
    {
        public BaseTenantValidator()
        {
            RuleFor(v => v.TenantId)
                .NotNull()
                .WithMessage("Tenant id cannot be null")
                .NotEmpty()
                .WithMessage("Tenant id is missing.");
        }
    }
}
