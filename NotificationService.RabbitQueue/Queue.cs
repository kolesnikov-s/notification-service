namespace NotificationService.RabbitQueue
{
    public class Queue
    {
        #if DEBUG
        public static string MessageQueue => "MessageQueueTest";
        #endif

        #if !DEBUG
        public static string MessageQueue => "MessageQueue";
        #endif
    }
}