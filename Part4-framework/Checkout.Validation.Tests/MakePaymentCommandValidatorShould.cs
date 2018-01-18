using System;
using System.Linq;
using Checkout.Commands;
using FluentValidation.Results;
using Xunit;

namespace Checkout.Validation.Tests
{
    public class MakePaymentCommandValidatorShould
    {
        [Fact]
        public void PassValidObject()
        {
            MakePaymentCommand command = new MakePaymentCommand
            {
                AuthenticatedUserId = Guid.NewGuid(),
                OrderId = Guid.NewGuid()
            };
            MakePaymentCommandValidator testSubject = new MakePaymentCommandValidator();

            ValidationResult result = testSubject.Validate(command);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void FailOnMissingOrderId()
        {
            MakePaymentCommand command = new MakePaymentCommand
            {
                AuthenticatedUserId = Guid.NewGuid(),
                OrderId = Guid.Empty
            };
            MakePaymentCommandValidator testSubject = new MakePaymentCommandValidator();

            ValidationResult result = testSubject.Validate(command);

            Assert.False(result.IsValid);
            Assert.Equal("OrderId", result.Errors.Single().PropertyName);
        }
    }
}
