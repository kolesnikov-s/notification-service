using NotificationService.Entities.Base;

namespace NotificationService.Entities
{
    public class EmailMessage: BaseEntity
    {
        public string Email { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
        public bool IsSent { get; set; }
    }
}