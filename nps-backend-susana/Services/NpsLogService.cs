using nps_backend_susana.Model.Dtos;
using nps_backend_susana.Model.Entities;
using nps_backend_susana.Model.Interfaces;
using System.Net;

namespace nps_backend_susana.Services
{
    public class NpsLogService
    {
        private readonly INpsLogRepository _npsLogRepository;
        private readonly HttpClient _httpClient;
        private const string systemId = "3c477fc7-0d4d-458a-6078-08dc43a0a620";
        private const string user = "susana.angeli.b";
        private const string urlQuestion = "https://nps-stg.ambevdevs.com.br/api/question/check";
        private const string urlCreate = "https://nps-stg.ambevdevs.com.br/api/survey/create";

        public NpsLogService(INpsLogRepository npsLogRepository, HttpClient httpClient)
        {
            _npsLogRepository = npsLogRepository;
            _httpClient = httpClient;
        }

        public async Task<string> BuscarPergunta()
        {
            var url = $"{urlQuestion}?user={user}&systemId={systemId}";

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("Authorization", systemId); 

            try
            {
                var response = await _httpClient.SendAsync(request); 
                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    return "Você já respondeu a pesquisa!";
                }
                else
                {
                    response.EnsureSuccessStatusCode();  
                    return await response.Content.ReadAsStringAsync();
                }
            }
            catch (HttpRequestException)
            {
                return "Erro ao buscar pergunta!";
            }
        }

        public async Task<bool> SalvarResposta(ScoreDto scoreDto)
        {
            Guid categoryId;

            switch (scoreDto.Category)
            {
                case "OTHER":
                    categoryId = Guid.Parse("8656aec6-9f0f-41e1-a94c-49e2d49a5492");
                    break;
                case "CRASH":
                    categoryId = Guid.Parse("e0001d6c-905e-42a0-8f2c-89184a6225da");
                    break;
                case "SLOWNESS":
                    categoryId = Guid.Parse("ab7e4d23-ce17-4049-9856-9f1cea110a7e");
                    break;
                case "INTERFACE":
                    categoryId = Guid.Parse("438109f9-c8bf-43b1-94a0-a186b758b1e1");
                    break;
                case "BUGS":
                    categoryId = Guid.Parse("883fdf80-70a2-4e36-bf0a-a291c1174cba");
                    break;
                case "CONNECTIVITY":
                    categoryId = Guid.Parse("25301326-f806-42e7-9fd9-4ea1e0ddf396");
                    break;
                default:
                    categoryId = Guid.Empty;
                    break;
            }

            var postData = new
            {
                createdDate = DateTime.UtcNow,
                scoreDto.Score,
                comments = scoreDto.Description,
                user,
                surveyType = 0,
                systemId,
                categoryId
            };

            var request = new HttpRequestMessage(HttpMethod.Post, urlCreate)
            {
                Content = JsonContent.Create(postData) 
            };
            request.Headers.Add("Authorization", systemId); 

            var response = await _httpClient.SendAsync(request); 

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            var npsLog = new NpsLog()
            {
                IdProduct = new Guid(systemId),
                DateScore = DateTime.Now,
                CategoryId = categoryId,
                ReasonDescription = scoreDto.Description,
                Score = scoreDto.Score,
                UserId = user
            };

            await _npsLogRepository.IncluirAsync(npsLog);

            return true;
        }
    }
}