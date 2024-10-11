using Moq;
using nps_backend_susana.Controllers;
using nps_backend_susana.Services;
using System.Net;

namespace nps_backend_susana_tests.Controllers
{
    public class UnitTest1
    {
        private readonly Mock<INpsLogService> _mockService;  
        private readonly NpsLogController _controller;

        public NpsLogControllerTests()
        {
            _mockService = new Mock<INpsLogService>();
            _controller = new NpsLogController(_mockService.Object);  // Injeta o mock
        }

        [Fact]
        public void Must_return_a_question()
        {
            // Arrange
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            var expectedResponse = "Esta é a pergunta de exemplo.";

            mockHttpMessageHandler
                .Setup(handler => handler.SendAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(expectedResponse)
                });

            var mockHttpClient = new HttpClient(mockHttpMessageHandler.Object);
            var service = new NpsLogService(mockHttpClient); // Classe contendo o método BuscarPergunta()

            // Act
            var result = service.BuscarPergunta();

            // Assert
            Assert.Equal(expectedResponse, result);
        }
    }
}
