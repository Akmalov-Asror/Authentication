using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Teaching.Common.SwaggerVersioning;

public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;

    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        => _provider = provider;

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var descriptions in _provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(descriptions.GroupName, CreateInfoForApiVersion(descriptions));
        }
    }
    private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
    {
        var info = new OpenApiInfo()
        {
            Title = "Teaching Web API",
            Version = description.ApiVersion.ToString(),
            Description = "Description for the Students Managment Web API",
            Contact = new OpenApiContact { Name = "Teaching", Email = "admin@gmail.com" },
            License = new OpenApiLicense { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
        };
        if (description.IsDeprecated)
        {
            info.Description += "This API version has been deprecated";
        }
        return info;
    }
}
