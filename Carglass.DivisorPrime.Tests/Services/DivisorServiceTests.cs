using Carglass.DivisorPrime.CLI.Dtos;
using Carglass.DivisorPrime.CLI.Interfaces;
using Carglass.DivisorPrime.CLI.Services;
using Moq;

public class DivisorServiceTests : IDisposable
{
    private readonly Mock<IDivisorApi> _mockDivisorApi;
    private readonly Mock<IResponseBuilder> _mockResponseBuilder;

    public DivisorServiceTests()
    {
        _mockDivisorApi = new Mock<IDivisorApi>();
        _mockResponseBuilder = new Mock<IResponseBuilder>();
    }

    [Fact]
    public async Task ShouldReturnCorrectDivisors()
    {
        // Configuração dos mocks
        _mockDivisorApi
            .Setup(api => api.GetAsync(It.IsAny<string>()))
            .ReturnsAsync(new ApiResponseDto
            {
                IsSuccess = true,
                Message = "Sucesso",
                Divisors = new DivisorsDto
                {
                    Number = 10,
                    Divisors = new[] { 1, 2, 5, 10 }.ToList(),
                    PrimeDivisors = new[] { 2, 5 }.ToList()
                }
            });

        _mockResponseBuilder
            .Setup(rb => rb.WithMessage(It.IsAny<string>()))
            .Returns(_mockResponseBuilder.Object);

        _mockResponseBuilder
            .Setup(rb => rb.AsError())
            .Returns(_mockResponseBuilder.Object);

        _mockResponseBuilder
            .Setup(rb => rb.AsSuccess())
            .Returns(_mockResponseBuilder.Object);

        _mockResponseBuilder
            .Setup(rb => rb.Print())
            .Callback(() => { });

        _mockResponseBuilder
            .Setup(rb => rb.WaitForExit())
            .Callback(() => { });


        // Execução do teste
        var service = new DivisorService(_mockDivisorApi.Object, _mockResponseBuilder.Object);
        var input = new[] { "10" };

        await service.ExecuteAsync(input);

        // Verificações
        _mockDivisorApi.Verify(api => api.GetAsync("10"), Times.Once);
        _mockResponseBuilder.Verify(rb => rb.WithMessage("Sucesso"), Times.Once);
        _mockResponseBuilder.Verify(rb => rb.Print(), Times.Exactly(1)); 
        _mockResponseBuilder.Verify(rb => rb.WaitForExit(), Times.Once);
    }

    [Fact]
    public async Task ShouldHandleEmptyParameterAndReturnErrorMessage()
    {
        // Arrange
        var errorMessage = "API: O número deve ser maior que zero.";

        _mockDivisorApi
            .Setup(api => api.GetAsync(It.IsAny<string>()))
            .ReturnsAsync(new ApiResponseDto
            {
                IsSuccess = false,
                Message = errorMessage
            });

        _mockResponseBuilder
            .Setup(rb => rb.WithMessage(It.IsAny<string>()))
            .Returns(_mockResponseBuilder.Object);

        _mockResponseBuilder
            .Setup(rb => rb.AsError())
            .Returns(_mockResponseBuilder.Object);

        _mockResponseBuilder
            .Setup(rb => rb.Print())
            .Callback(() => { });

        _mockResponseBuilder
            .Setup(rb => rb.WaitForExit())
            .Callback(() => { });

        // Execução do serviço
        var service = new DivisorService(_mockDivisorApi.Object, _mockResponseBuilder.Object);
        var input = new[] { "" }; // Parâmetro vazio

        await service.ExecuteAsync(input);

        // Verificações
        _mockDivisorApi.Verify(api => api.GetAsync(""), Times.Once);
        _mockResponseBuilder.Verify(rb => rb.WithMessage($"API: O número deve ser maior que zero."), Times.Once);
        _mockResponseBuilder.Verify(rb => rb.AsError(), Times.Once);
        _mockResponseBuilder.Verify(rb => rb.Print(), Times.Once);
        _mockResponseBuilder.Verify(rb => rb.WaitForExit(), Times.Once);
    }

    [Fact]
    public async Task ShouldHandleInvalidParameterAndReturnErrorMessage()
    {
        // Arrange
        var invalidInput = "B"; // Parâmetro inválido
        var errorMessage = "[Erro]: API: O número informado é inválido. Certifique-se de inserir um número inteiro válido.";

        _mockDivisorApi
            .Setup(api => api.GetAsync(invalidInput))
            .ReturnsAsync(new ApiResponseDto
            {
                IsSuccess = false,
                Message = errorMessage
            });

        _mockResponseBuilder
            .Setup(rb => rb.WithMessage(It.IsAny<string>()))
            .Returns(_mockResponseBuilder.Object);

        _mockResponseBuilder
            .Setup(rb => rb.AsError())
            .Returns(_mockResponseBuilder.Object);

        _mockResponseBuilder
            .Setup(rb => rb.Print())
            .Callback(() => { });

        _mockResponseBuilder
            .Setup(rb => rb.WaitForExit())
            .Callback(() => { /* Simulação de comportamento, se necessário */ });

        var service = new DivisorService(_mockDivisorApi.Object, _mockResponseBuilder.Object);

        // Act
        await service.ExecuteAsync(new[] { invalidInput });

        // Assert
        _mockDivisorApi.Verify(api => api.GetAsync(invalidInput), Times.Once);
        _mockResponseBuilder.Verify(rb => rb.WithMessage(errorMessage), Times.Once);
        _mockResponseBuilder.Verify(rb => rb.AsError(), Times.Once);
        _mockResponseBuilder.Verify(rb => rb.Print(), Times.Once);
        _mockResponseBuilder.Verify(rb => rb.WaitForExit(), Times.Once);
        _mockResponseBuilder.VerifyNoOtherCalls();
    }


    public void Dispose()
    {
        _mockDivisorApi.VerifyNoOtherCalls();
        _mockResponseBuilder.VerifyNoOtherCalls();
    }
}
