using System;
using Checkout.Commands;
using FluentValidation;

namespace Checkout.Validation
{
    class MakePaymentCommandValidator : AbstractValidator<MakePaymentCommand>
    {
        public MakePaymentCommandValidator()
        {
            RuleFor(c => c.OrderId).NotEqual(Guid.Empty);
        }
    }
}
