namespace desafio.Models.Entites
{
    public class Subscription
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long StatusId { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}