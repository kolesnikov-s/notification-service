namespace NotificationService.Application.Models
{
    public class SendMessageRequest
    {
        public string Type { get; set; }
        public string Contact { get; set; }
        public string Text { get; set; }
    }
}