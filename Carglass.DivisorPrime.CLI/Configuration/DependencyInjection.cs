using Carglass.DivisorPrime.CLI.Builders;
using Carglass.DivisorPrime.CLI.Integrations.Apis;
using Carglass.DivisorPrime.CLI.Interfaces;
using Carglass.DivisorPrime.CLI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public static class DependencyInjection
{
    public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
    {

        // Configurando o HttpClient com a BaseUrl do appsettings.json
        services.AddHttpClient<IDivisorApi, DivisorApi>(client =>
        {
            var baseAddress = configuration["ApiSettings:BaseUrl"];

            if (string.IsNullOrWhiteSpace(baseAddress))
            {
                throw new InvalidOperationException(
                    "O endereço base para a API Divisor está ausente ou inválido. Verifique o appsettings.json.");
            }

            client.BaseAddress = new Uri(baseAddress);
        });

        // Registrando serviços e dependências
        services.AddSingleton(configuration);
        services.AddScoped<IDivisorService, DivisorService>();
        services.AddScoped<IResponseBuilder, ResponseBuilder>();

        return services;
    }
}
