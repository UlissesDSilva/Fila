using desafio.Models.Enums;

namespace desafio.Models.Entites
{
    public class EventHistory
    {
        public long Id { get; set; }
        public long SubscriptionId { get; set; }
        public Subscription? Subscription { get; set; }
        public TypeOfNotifications Type { get; set; }
        public DateTime CreateAt { get; set; }
    }
}