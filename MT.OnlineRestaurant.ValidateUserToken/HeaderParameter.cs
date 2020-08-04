using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace MT.OnlineRestaurant.ValidateUserToken
{
    public class HeaderParameter : IOperationFilter
    {
        /// <summary>
        /// confuguration for header in swagger testing
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var controllerActionDescriptor = context.ApiDescription.ActionDescriptor as ControllerActionDescriptor;
            if (!SkipAuthorization(controllerActionDescriptor.MethodInfo))
            {
                if (operation.Parameters == null)
                    operation.Parameters = new List<OpenApiParameter>();

                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = "AuthToken",
                    In = ParameterLocation.Header,
                    Schema = new OpenApiSchema
                    {
                        Type = "String"
                        
                    },
                    Required = false // set to false if this is optional
                });

                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = "CustomerId",
                    In = ParameterLocation.Header,
                    Schema = new OpenApiSchema
                    {
                        Type = "int"

                    },
                    Required = false // set to false if this is optional
                });
            }
        }

        private bool SkipAuthorization(MethodInfo methodList)
        {
            string AllowAnonymous = "Microsoft.AspNetCore.Authorization.AllowAnonymousAttribute";
            if (methodList != null)
            {
                var actionAttributes = methodList.GetCustomAttributes(inherit: true);
                foreach (var attribute in actionAttributes)
                {
                    string type = attribute.GetType().ToString();
                    if (type == AllowAnonymous)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

    }
}
