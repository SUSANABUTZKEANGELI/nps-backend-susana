namespace nps_backend_susana.Model.Entities
{
    public class NpsLog 
    {
        public int Id { get; set; }
        public DateTimeOffset DateScore { get; set; }
        public Guid IdProduct { get; set; }
        public int Score { get; set; }
        public string ReasonDescription { get; set; }
        public Guid? CategoryId { get; set; }
        public string UserId { get; set; }
    }
}
