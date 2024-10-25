using Newtonsoft.Json;

namespace nps_backend_susana.Model.Dtos
{
    public class ScoreDto
    {
        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("selectedRatingScale")]
        public int Score { get; set; }

        [JsonProperty("reasonDescription")]
        public string Description { get; set; }

        [JsonProperty("selectedCategory")]
        public int CategoryId { get; set; }
    }
}
