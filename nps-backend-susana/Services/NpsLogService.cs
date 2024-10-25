using nps_backend_susana.Model.Dtos;
using nps_backend_susana.Model.Entities;
using nps_backend_susana.Model.Enums;
using nps_backend_susana.Model.Interfaces;
using nps_backend_susana.Model.Responses;
using System.Net;

namespace nps_backend_susana.Services
{
    public class NpsLogService : INpsLogService
    {
        private readonly INpsLogRepository _npsLogRepository;
        private readonly HttpClient _httpClient;
        private const string systemId = "3c477fc7-0d4d-458a-6078-08dc43a0a620";
        private const string urlQuestion = "https://nps-stg.ambevdevs.com.br/api/question/check";
        private const string urlCreate = "https://nps-stg.ambevdevs.com.br/api/survey/create";

        public NpsLogService(INpsLogRepository npsLogRepository, HttpClient httpClient)
        {
            _npsLogRepository = npsLogRepository;
            _httpClient = httpClient;
        }

        public async Task<NpsResponse> BuscarPergunta(string login)
        {
            if (string.IsNullOrEmpty(login))
            {
                return new NpsResponse
                {
                    Success = false,
                    Message = "Invalid Login!"
                };
            }

            var url = $"{urlQuestion}?user={login}&systemId={systemId}";

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("Authorization", systemId); 

            try
            {
                var response = await _httpClient.SendAsync(request); 
                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    return new NpsResponse
                    {
                        Success = false,
                        Message = "You´ve already answered the review!"
                    };
                }
                else
                {
                    response.EnsureSuccessStatusCode();  
                    var message = await response.Content.ReadAsStringAsync();
                    return new NpsResponse
                    {
                        Success = true,
                        Message = message
                    };
                }
            }
            catch (HttpRequestException)
            {
                return new NpsResponse
                {
                    Success = false,
                    Message = "An error occurred while finding your question"
                };
            }
        }

        public async Task<NpsResponse> SalvarResposta(ScoreDto scoreDto)
        {
            if (scoreDto == null)
            {
                return new NpsResponse
                {
                    Success = false,
                    Message = "Invalid score!"
                };
            }
            
            if (scoreDto.Score <= 6 &&
               (scoreDto.CategoryId == null || scoreDto.CategoryId == 0 || scoreDto.CategoryId > 6))
            {
                return new NpsResponse
                {
                    Success = false,
                    Message = "Invalid category!"
                };
            }

            Category category = (Category)Enum.ToObject(typeof(Category), scoreDto.CategoryId);
            var categoryId = Guid.Parse(Model.Extensions.EnumExtensions.GetTranslation(category));

            var postData = new
            {
                createdDate = DateTime.UtcNow,
                score = scoreDto.Score,
                comments = scoreDto.Description,
                user = scoreDto.Login,
                surveyType = 0,
                systemId = systemId,
                categoryId = categoryId
            };

            var request = new HttpRequestMessage(HttpMethod.Post, urlCreate)
            {
                Content = JsonContent.Create(postData) 
            };
            request.Headers.Add("Authorization", systemId); 

            var response = await _httpClient.SendAsync(request); 

            if (!response.IsSuccessStatusCode)
            {
                return new NpsResponse
                {
                    Success = false,
                    Message = "An error occurred while submitting your review!"
                };
            }

            var npsLog = new NpsLog()
            {
                IdProduct = new Guid(systemId),
                DateScore = DateTime.Now,
                CategoryId = categoryId,
                ReasonDescription = scoreDto.Description,
                Score = scoreDto.Score,
                UserId = scoreDto.Login
            };

            await _npsLogRepository.IncluirAsync(npsLog);

            return new NpsResponse
            {
                Success = true,
                Message = "Thank you for your participation!"
            };
        }
    }
}