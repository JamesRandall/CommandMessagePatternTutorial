using System.Linq;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace OnlineStore.Api.Swagger
{
    public class SwaggerAuthenticatedUserIdOperationFilter : IOperationFilter
    {
        private const string AuthenticatedUserIdProperty = "authenticateduserid";

        public void Apply(Operation operation, OperationFilterContext context)
        {
            IParameter authenticatedUserIdParameter = operation.Parameters?.SingleOrDefault(x => x.Name.ToLower() == AuthenticatedUserIdProperty);
            if (authenticatedUserIdParameter != null)
            {
                operation.Parameters.Remove(authenticatedUserIdParameter);
            }
        }
    }
}
