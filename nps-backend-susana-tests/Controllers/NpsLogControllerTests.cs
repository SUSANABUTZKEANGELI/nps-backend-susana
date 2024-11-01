﻿using Microsoft.AspNetCore.Mvc;
using nps_backend_susana.Controllers;
using nps_backend_susana.Model.Dtos;
using nps_backend_susana.Model.Interfaces;
using nps_backend_susana.Model.Responses;
using NSubstitute;

namespace nps_backend_susana_tests.Repositories
{
    public class NpsLogControllerTests 
    {
        [Fact]
        public async Task Shoul_insert_score_log()
        {
            var iLogService = Substitute.For<INpsLogService>();

            var controller = new NpsLogController(iLogService);
            var scoreDto = new ScoreDto{
                Login = "teste",
                Score = 10, 
                Description = "dez",
                CategoryId = 0};

            var response = new NpsResponse();

            iLogService.SalvarResposta(scoreDto).Returns(response);
            var result = await controller.SalvarResposta(scoreDto);

            var okResult = result as OkObjectResult;
            
            Assert.Equal("Nota e log salvos com sucesso.", okResult.Value);
        }

        [Fact]
        public async Task Shoul_not_insert_score_log_with_invalid_category()
        {
            var iLogService = Substitute.For<INpsLogService>();

            var controller = new NpsLogController(iLogService);
            var scoreDto = new ScoreDto
            {
                Login = "teste",
                Score = 6,
                Description = "seis",
                CategoryId = 0
            };

            var response = new NpsResponse();

            iLogService.SalvarResposta(scoreDto).Returns(response);

            var result = await controller.SalvarResposta(scoreDto);

            var badRequestObjectResult = result as BadRequestObjectResult;

            Assert.Equal("Categoria inválida", badRequestObjectResult.Value);
        }

        [Fact]
        public async Task Shoul_not_insert_score_log_with_error()
        {
            var iLogService = Substitute.For<INpsLogService>();

            var controller = new NpsLogController(iLogService);
            var scoreDto = new ScoreDto
            {
                Login = "teste",
                Score = 6,
                Description = "seis",
                CategoryId = 1
            };

            var response = new NpsResponse();

            iLogService.SalvarResposta(scoreDto).Returns(response);

            var result = await controller.SalvarResposta(scoreDto);

            var objectResult = result as ObjectResult;

            Assert.Equal("Erro ao salvar a nota.", objectResult.Value);
        }
    }
}
