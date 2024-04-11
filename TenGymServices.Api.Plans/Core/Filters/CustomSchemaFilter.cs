using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TenGymServices.Api.Plans.Core.Filters
{
    public class CustomSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {

            if (schema.Enum != null && context.Type.IsEnum)
            {
                var enumValues = new List<IOpenApiAny>();
                foreach (var enumMember in Enum.GetNames(context.Type))
                {
                    var enumValue = new OpenApiString(Enum.Parse(context.Type, enumMember).ToString());
                    enumValues.Add(enumValue);
                }
                schema.Enum = enumValues;
            }
           
           
        }
    }
}