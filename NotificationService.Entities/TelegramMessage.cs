using NotificationService.Entities.Base;

namespace NotificationService.Entities
{
    public class TelegramMessage: BaseEntity
    {
        public string ChatId { get; set; }
        public string Text { get; set; }
        public bool IsSent { get; set; }
    }
}