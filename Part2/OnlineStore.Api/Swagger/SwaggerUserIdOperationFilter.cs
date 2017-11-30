using System.Linq;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace OnlineStore.Api.Swagger
{
    public class SwaggerUserIdOperationFilter : IOperationFilter
    {
        private const string UserIdProperty = "userid";
        public void Apply(Operation operation, OperationFilterContext context)
        {
            IParameter userIdParameter = operation.Parameters?.SingleOrDefault(x => x.Name.ToLower() == UserIdProperty);
            if (userIdParameter != null)
            {
                operation.Parameters.Remove(userIdParameter);
            }
        }
    }
}
