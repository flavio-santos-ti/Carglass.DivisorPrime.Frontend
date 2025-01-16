using Carglass.DivisorPrime.CLI.Dtos;

namespace Carglass.DivisorPrime.CLI.Interfaces;

public interface IResponseBuilder
{
    IResponseBuilder WithMessage(string message);
    IResponseBuilder AsError();
    IResponseBuilder AsSuccess();
    void Print();
    void WaitForExit();
    ApiResponseDto Build();
}
