namespace desafio.Models.Entites
{
    public class EventHistory
    {
        public long Id { get; set; }
        public long SubscriptionId { get; set; }
        public string Type { get; set; } = string.Empty;
        public DateTime CreateAt { get; set; }
    }
}