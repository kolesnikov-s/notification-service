using NotificationService.Entities.Base;

namespace NotificationService.Entities
{
    public class SmsMessage: BaseEntity
    {
        public string Phone { get; set; }
        public string Text { get; set; }
        public bool IsSent { get; set; }
    }
}