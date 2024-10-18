using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using nps_backend_susana.Model.Entities;
using nps_backend_susana.Model.Interfaces;
using nps_backend_susana.Model.Repositories;
using nps_backend_susana.Services;
using NSubstitute;

namespace nps_backend_susana_tests.Repositories
{
    public class NpsLogRepositoryTests
    {
        private readonly INpsLogRepository _repository;
        private readonly Contexto _contexto;
        private readonly HttpClient _httpClient;

        public NpsLogRepositoryTests()
        {
            _contexto = Substitute.For<Contexto>();
            _repository = new NpsLogRepository(_contexto);
            _httpClient = new HttpClient();
        }

        [Fact]
        public async Task Shoul_insert_score_log()
        {
            var npsLog = new NpsLog()
            {
                Score = 10,
                ReasonDescription = "dez",
                CategoryId = Guid.NewGuid(),
                DateScore = DateTime.UtcNow,
                Id = 1,
                IdProduct = Guid.NewGuid(),
                UserId = "teste"
            };

            var options = new DbContextOptionsBuilder<Contexto>()
                .UseInMemoryDatabase(databaseName: "Teste")
                .Options;

            using var context = new Contexto(options);
            var repository = new NpsLogRepository(context);

            await repository.IncluirAsync(npsLog);

            var result = await context.NpsLog.FirstOrDefaultAsync(l => l.Id == npsLog.Id);
            result.ReasonDescription.Should().Be("Dez");
        }
    }
}
