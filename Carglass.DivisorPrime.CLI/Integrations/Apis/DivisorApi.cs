using Carglass.DivisorPrime.CLI.Dtos;
using Carglass.DivisorPrime.CLI.Interfaces;
using System.Text.Json;

namespace Carglass.DivisorPrime.CLI.Integrations.Apis
{
    public class DivisorApi : IDivisorApi
    {
        private readonly HttpClient _client;
        private readonly IResponseBuilder _responseBuilder;

        public DivisorApi(HttpClient client, IResponseBuilder responseBuilder)
        {
            _client = client;
            _responseBuilder = responseBuilder;
        }

        public async Task<ApiResponseDto> GetAsync(string numero)
        {
            try
            {
                var response = await _client.GetAsync($"/api/Divisor/{numero}");

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var responseApi = JsonSerializer.Deserialize<ApiResponseDto>(await response.Content.ReadAsStringAsync(), options);

                if (responseApi == null)
                {
                    return _responseBuilder
                        .WithMessage("A resposta da API foi vazia.")
                        .AsError()
                        .Build();
                }

                if (!responseApi.IsSuccess)
                {
                    return _responseBuilder
                        .WithMessage($"API: {responseApi.Message}")
                        .AsError()
                        .Build();
                }

                return responseApi;
            }
            catch (Exception ex)
            {
                return _responseBuilder
                    .WithMessage($"Ocorreu um erro ao chamar a API: {ex.Message}")
                    .AsError()
                    .Build();
            }
        }
    }
}
