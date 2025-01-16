using Carglass.DivisorPrime.CLI.Dtos;

namespace Carglass.DivisorPrime.CLI.Interfaces
{
    public interface IDivisorApi
    {
        Task<ApiResponseDto> GetAsync(string numero);
    }
}
