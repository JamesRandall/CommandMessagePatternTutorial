using System;
using FluentValidation;
using ShoppingCart.Commands;

namespace ShoppingCart.Validation
{
    internal class AddToCartCommandValidator : AbstractValidator<AddToCartCommand>
    {
        public AddToCartCommandValidator()
        {
            RuleFor(c => c.ProductId).NotEqual(Guid.Empty);
            RuleFor(c => c.Quantity).GreaterThan(0);
        }
    }
}
