using System;
using System.Linq;
using FluentValidation.Results;
using ShoppingCart.Commands;
using Xunit;

namespace ShoppingCart.Validation.Tests
{
    public class AddToCartCommandValidatorShould
    {
        [Fact]
        public void PassValidObject()
        {
            AddToCartCommand command = new AddToCartCommand
            {
                AuthenticatedUserId = Guid.NewGuid(),
                ProductId = Guid.NewGuid(),
                Quantity = 1
            };
            AddToCartCommandValidator testSubject = new AddToCartCommandValidator();

            ValidationResult result = testSubject.Validate(command);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void FailOnInvalidQuantity()
        {
            AddToCartCommand command = new AddToCartCommand
            {
                AuthenticatedUserId = Guid.NewGuid(),
                ProductId = Guid.NewGuid(),
                Quantity = 0
            };
            AddToCartCommandValidator testSubject = new AddToCartCommandValidator();

            ValidationResult result = testSubject.Validate(command);

            Assert.False(result.IsValid);
            Assert.Equal("Quantity", result.Errors.Single().PropertyName);
        }

        [Fact]
        public void FailOnMissingProductId()
        {
            AddToCartCommand command = new AddToCartCommand
            {
                AuthenticatedUserId = Guid.NewGuid(),
                ProductId = Guid.Empty,
                Quantity = 1
            };
            AddToCartCommandValidator testSubject = new AddToCartCommandValidator();

            ValidationResult result = testSubject.Validate(command);

            Assert.False(result.IsValid);
            Assert.Equal("ProductId", result.Errors.Single().PropertyName);
        }
    }
}
