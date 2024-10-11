using MediatR;
using Microsoft.AspNetCore.Mvc;
using nps_backend_susana.Controllers;
using nps_backend_susana.Services;
using System.ComponentModel.DataAnnotations;

namespace nps_backend_susana_tests.Repositories
{
    public class NpsLogRepositoryTests
    {
        private readonly NpsLogController _controller;
        private readonly NpsLogService _npsLogService;
        private readonly IMediator _mediator;
        private readonly Fixture _fixture;

        public NpsLogRepositoryTests()
        {
            _mediator = Substitute.For<IMediator>();
            _controller = new NpsLogController(_npsLogService);
        }

        [Fact]
        public async Task Shoul_insert_score_log()
        {
            var request = _fixture.Create<CreatePaymentMethodRequest>();

            _mediator
            .Send(Arg.Any<CreatePaymentMethodRequest>())
               .Returns(new CreatePaymentMethodResult(true, new ValidationResult()));

            var result = await _controller.Create(request);

            Assert.IsType<OkObjectResult>(result);
        }
    }
}
