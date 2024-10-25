using FluentAssertions;
using Moq;
using Moq.Protected;
using nps_backend_susana.Model.Interfaces;
using nps_backend_susana.Model.Repositories;
using nps_backend_susana.Services;
using NSubstitute;
using NSubstitute.Extensions;
using System.Net;

namespace nps_backend_susana_tests.Services
{
    public class NpsLogServiceTests
    {
        private readonly NpsLogService _npsLogService;
        private readonly HttpClient _httpClient;
        private readonly INpsLogRepository _repository;
        private readonly Contexto _contexto;
        private const string urlQuestion = "https://nps-stg.ambevdevs.com.br/api/question/check";
        private const string urlCreate = "https://nps-stg.ambevdevs.com.br/api/survey/create";
        private const string systemId = "3c477fc7-0d4d-458a-6078-08dc43a0a620";
        private const string user = "susana.angeli.d";

        public NpsLogServiceTests()
        {
            _contexto = Substitute.For<Contexto>();
            _repository = new NpsLogRepository(_contexto);
            _httpClient = Substitute.For<HttpClient>();
            _npsLogService = new NpsLogService(_repository, _httpClient);
        }

        [Fact]
        public async Task Should_not_return_a_question()
        {
            var httpResponse = new HttpResponseMessage(HttpStatusCode.OK);

            _httpClient.ReturnsForAll(httpResponse);

            var result = await _npsLogService.BuscarPergunta(user);

            result.Should().BeSameAs("Você já respondeu a pesquisa!");
        }
    }
}
