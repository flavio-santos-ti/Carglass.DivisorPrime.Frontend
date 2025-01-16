using Carglass.DivisorPrime.CLI.Dtos;
using Carglass.DivisorPrime.CLI.Integrations.Apis;
using Carglass.DivisorPrime.CLI.Interfaces;
using Moq;
using Moq.Protected;
using System.Net;

namespace Carglass.DivisorPrime.Tests.Integrations.Apis
{
    public class DivisorApiTests : IDisposable
    {
        private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
        private readonly Mock<IResponseBuilder> _mockResponseBuilder;
        private readonly HttpClient _httpClient;
        private readonly DivisorApi _divisorApi;

        public DivisorApiTests()
        {
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            _mockResponseBuilder = new Mock<IResponseBuilder>();

            _httpClient = new HttpClient(_mockHttpMessageHandler.Object)
            {
                BaseAddress = new Uri("http://test.com")
            };

            _divisorApi = new DivisorApi(_httpClient, _mockResponseBuilder.Object);
        }

        [Fact]
        public async Task GetAsync_ShouldReturnSuccessResponse_WhenApiReturnsValidData()
        {
            // Arrange
            var responseContent = "{\"isSuccess\": true, \"message\": \"Operação realizada com sucesso\", \"divisors\": {\"number\": 10, \"divisors\": [1, 2, 5, 10], \"primeDivisors\": [2, 5]}}";

            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(responseContent)
                });

            // Act
            var result = await _divisorApi.GetAsync("10");

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.Equal("Operação realizada com sucesso", result.Message); // Ajustado para a nova mensagem

            // Verifica se SendAsync foi chamado corretamente
            _mockHttpMessageHandler
                .Protected()
                .Verify("SendAsync",
                    Times.Once(),
                    ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get && req.RequestUri.ToString().Contains("/api/Divisor/10")),
                    ItExpr.IsAny<CancellationToken>());
        }


        [Fact]
        public async Task GetAsync_ShouldReturnErrorResponse_WhenApiReceivesInvalidParameter()
        {
            // Arrange
            var invalidInput = "B";
            var expectedErrorMessage = "API: O número informado é inválido. Certifique-se de inserir um número inteiro válido.";
            var responseContent = $"{{\"isSuccess\":false,\"message\":\"{expectedErrorMessage}\",\"statusCode\":400,\"divisors\":null}}";

            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req =>
                        req.Method == HttpMethod.Get &&
                        req.RequestUri == new Uri($"http://test.com/api/Divisor/{invalidInput}")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = new StringContent(responseContent)
                });

            _mockResponseBuilder
                .Setup(rb => rb.WithMessage(It.Is<string>(msg => msg.Contains(expectedErrorMessage))))
                .Returns(_mockResponseBuilder.Object);

            _mockResponseBuilder
                .Setup(rb => rb.AsError())
                .Returns(_mockResponseBuilder.Object);

            _mockResponseBuilder
                .Setup(rb => rb.Build())
                .Returns(new ApiResponseDto
                {
                    IsSuccess = false,
                    Message = $"[Erro]: {expectedErrorMessage}",
                    Divisors = null
                });

            // Act
            var result = await _divisorApi.GetAsync(invalidInput);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal($"[Erro]: {expectedErrorMessage}", result.Message);
            Assert.Null(result.Divisors); // Verifica que a propriedade Divisors está nula

            // Verificar histórico da requisição
            _mockHttpMessageHandler
                .Protected()
                .Verify(
                    "SendAsync",
                    Times.Once(),
                    ItExpr.Is<HttpRequestMessage>(req =>
                        req.Method == HttpMethod.Get &&
                        req.RequestUri == new Uri($"http://test.com/api/Divisor/{invalidInput}")),
                    ItExpr.IsAny<CancellationToken>());

            _mockResponseBuilder.Verify(rb => rb.WithMessage(It.Is<string>(msg => msg.Contains(expectedErrorMessage))), Times.Once);
            _mockResponseBuilder.Verify(rb => rb.AsError(), Times.Once);
            _mockResponseBuilder.Verify(rb => rb.Build(), Times.Once);
            _mockResponseBuilder.VerifyNoOtherCalls(); // Garante que nenhum outro método foi chamado
        }

        public void Dispose()
        {
            // Verifica se nenhum outro método foi chamado nos mocks
            _mockHttpMessageHandler.VerifyNoOtherCalls();
            _mockResponseBuilder.VerifyNoOtherCalls();

            // Descarta o HttpClient
            _httpClient.Dispose();
        }
    }
}
