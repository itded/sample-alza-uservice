using Microsoft.Extensions.Options;

namespace Alza.UService.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(cfg =>
        {
            cfg.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
            {
                Title = "Alza REST uService - v1.0",
                Version = "v1",
            });
            cfg.CustomSchemaIds(type => type.FullName);
        });
        return services;
    }
}
