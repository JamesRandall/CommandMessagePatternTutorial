using System;
using FluentValidation;
using Store.Commands;

namespace Store.Validation
{
    internal class GetStoreProductQueryValidator : AbstractValidator<GetStoreProductQuery>
    {
        public GetStoreProductQueryValidator()
        {
            RuleFor(c => c.ProductId).NotEqual(Guid.Empty);
        }
    }
}
