using System.Diagnostics.CodeAnalysis;

namespace Carglass.DivisorPrime.CLI.Dtos;

[ExcludeFromCodeCoverage]
public class ApiResponseDto
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = string.Empty;
    public int StatusCode { get; set; }
    public DivisorsDto? Divisors { get; set; }
}
