using desafio.Models.Enums;

namespace desafio.Models.Entites
{
    public class Status
    {
        public long Id { get; set; }
        public TypeOfNotifications StatusType { get; set; }
    }
}