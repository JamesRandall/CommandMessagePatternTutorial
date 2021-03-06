﻿using System;
using Core.Model;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace OnlineStore.Api.Swagger
{
    public class SwaggerAuthenticatedUserIdFilter : ISchemaFilter
    {
        private const string AuthenticatedUserIdProperty = "authenticatedUserId";
        private static readonly Type UserContextCommandType = typeof(IUserContextCommand);

        public void Apply(Schema model, SchemaFilterContext context)
        {
            if (UserContextCommandType.IsAssignableFrom(context.SystemType))
            {
                if (model.Properties.ContainsKey(AuthenticatedUserIdProperty))
                {
                    model.Properties.Remove(AuthenticatedUserIdProperty);
                }
            }
        }
    }
}
