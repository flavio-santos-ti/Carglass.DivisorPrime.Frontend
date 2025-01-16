using System.Diagnostics.CodeAnalysis;

namespace Carglass.DivisorPrime.CLI.Dtos;

[ExcludeFromCodeCoverage]
public class DivisorsDto
{
    public int Number { get; set; }
    public List<int>? Divisors { get; set; }
    public List<int>? PrimeDivisors { get; set; }
}
