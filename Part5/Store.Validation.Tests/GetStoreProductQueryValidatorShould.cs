using System;
using FluentValidation.Results;
using Store.Commands;
using Xunit;

namespace Store.Validation.Tests
{
    public class GetStoreProductQueryValidatorShould
    {
        [Fact]
        public void PassValidObject()
        {
            GetStoreProductQuery query = new GetStoreProductQuery
            {
                ProductId = Guid.NewGuid()
            };
            GetStoreProductQueryValidator testSubject = new GetStoreProductQueryValidator();

            ValidationResult result = testSubject.Validate(query);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void FailsOnEmptyProductId()
        {
            GetStoreProductQuery query = new GetStoreProductQuery
            {
                ProductId = Guid.Empty
            };
            GetStoreProductQueryValidator testSubject = new GetStoreProductQueryValidator();

            ValidationResult result = testSubject.Validate(query);

            Assert.False(result.IsValid);
            Assert.Equal("ProductId", result.Errors[0].PropertyName);
        }
    }
}
