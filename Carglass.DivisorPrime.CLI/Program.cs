using Carglass.DivisorPrime.CLI.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;


[ExcludeFromCodeCoverage]
class Program
{
    static async Task Main(string[] args)
    {
        string appSettingsPath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");

        // Verifica se o arquivo appsettings.json existe
        if (!File.Exists(appSettingsPath))
        {
            Console.WriteLine($"Erro: O arquivo '{appSettingsPath}' não foi encontrado no diretório base.");
            return;
        }

        // Construindo as configurações do appsettings.json
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        // Configurando o container de dependências
        var services = new ServiceCollection();
        services.AddDependencies(configuration);

        // Criando o service provider
        var serviceProvider = services.BuildServiceProvider();

        var baseUrl = configuration["ApiSettings:BaseUrl"];
        if (string.IsNullOrWhiteSpace(baseUrl))
        {
            Console.WriteLine("Erro: A chave 'ApiSettings:BaseUrl' não está configurada no appsettings.json.");
            return;
        }

        // Resgatando e executando o serviço principal
        var service = serviceProvider.GetRequiredService<IDivisorService>();
        await service.ExecuteAsync(args);
    }
}
