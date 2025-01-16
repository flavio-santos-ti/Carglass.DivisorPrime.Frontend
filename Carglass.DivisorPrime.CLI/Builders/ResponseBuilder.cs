using Carglass.DivisorPrime.CLI.Dtos;
using Carglass.DivisorPrime.CLI.Interfaces;
using System.Text;

namespace Carglass.DivisorPrime.CLI.Builders;

public class ResponseBuilder : IResponseBuilder
{
    private readonly StringBuilder _messages = new StringBuilder();
    private bool _headerAdded = false;
    private bool _isSuccess = true; 

    public IResponseBuilder WithMessage(string message)
    {
        _messages.AppendLine(message);
        return this;
    }

    public IResponseBuilder AsError()
    {
        if (!_headerAdded && !_messages.ToString().StartsWith("[Erro]: "))
        {
            _messages.Insert(0, "[Erro]: ");
            _headerAdded = true;
        }
        _isSuccess = false;
        return this;
    }

    public IResponseBuilder AsSuccess()
    {
        if (!_headerAdded)
        {
            _messages.Insert(0, "[Sucesso]: ");
            _headerAdded = true;
        }

        _isSuccess = true;
        return this;
    }

    public ApiResponseDto Build()
    {
        return new ApiResponseDto
        {
            IsSuccess = _isSuccess,
            Message = _messages.ToString().Trim()
        };
    }

    public void Print()
    {
        Console.WriteLine(_messages.ToString());
    }

    public void WaitForExit()
    {
        Console.WriteLine("Pressione qualquer tecla para fechar...");
        Console.ReadLine();
    }
}
