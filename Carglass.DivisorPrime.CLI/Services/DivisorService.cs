using Carglass.DivisorPrime.CLI.Interfaces;

namespace Carglass.DivisorPrime.CLI.Services
{
    public class DivisorService : IDivisorService
    {
        private readonly IDivisorApi _divisorApi;
        private readonly IResponseBuilder _responseBuilder;

        public DivisorService(IDivisorApi divisorApi, IResponseBuilder responseBuilder)
        {
            _divisorApi = divisorApi;
            _responseBuilder = responseBuilder;
        }

        public async Task ExecuteAsync(string[] args)
        {
            try
            {
                var input = args.Length > 0 ? args[0] : string.Empty;

                var responseApi = await _divisorApi.GetAsync(input);

                if (!responseApi.IsSuccess)
                {
                    _responseBuilder
                        .WithMessage(responseApi.Message)
                        .AsError()
                        .Print();

                    return;
                }

                _responseBuilder
                    .WithMessage(responseApi.Message)
                    .WithMessage($"Número: {responseApi.Divisors?.Number}")
                    .WithMessage($"Divisores: {string.Join(", ", responseApi.Divisors?.Divisors ?? new List<int>())}")
                    .WithMessage($"Divisores Primos: {string.Join(", ", responseApi.Divisors?.PrimeDivisors ?? new List<int>())}")
                    .AsSuccess()
                    .Print();
            }
            catch (Exception ex)
            {
                _responseBuilder
                    .WithMessage($"Ocorreu um erro: {ex.Message}")
                    .AsError()
                    .Print();
            }
            finally
            {
                _responseBuilder.WaitForExit();
            }
        }
    }
}
