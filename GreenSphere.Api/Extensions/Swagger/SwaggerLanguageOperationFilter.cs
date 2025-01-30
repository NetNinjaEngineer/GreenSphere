using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace GreenSphere.Api.Extensions.Swagger
{
    public class SwaggerLanguageOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters ??= [];

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "Accept-Language",
                In = ParameterLocation.Header,
                Required = false,
                Schema = new OpenApiSchema
                {
                    Type = "string",
                    Enum =
                    [
                        new OpenApiString("en-US"),
                        new OpenApiString("ar-EG")
                    ],
                    Default = new OpenApiString("en-US")
                }
            });
        }
    }
}