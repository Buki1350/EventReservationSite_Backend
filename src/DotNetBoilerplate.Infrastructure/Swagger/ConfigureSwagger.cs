using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DotNetBoilerplate.Infrastructure.Swagger;

internal sealed class ConfigureSwagger
{
    public static void ConfigureSwaggerOptions(SwaggerGenOptions swagger)
    {
        swagger.EnableAnnotations();
        swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });

        swagger.SchemaFilter<EnumSchemaFilter>();

        swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description =
                "Input your JWT Authorization header to access this API. Example: \"Authorization: Bearer {token}\"",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });

        swagger.CustomSchemaIds(type => type.FullName?.Replace('+', '.'));
        swagger.SupportNonNullableReferenceTypes();
    }
}