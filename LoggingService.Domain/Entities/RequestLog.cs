namespace LoggingService.Domain.Entities
{
    public class RequestLog
    {
        public string Id { get; set; }=string.Empty;
        public string Endpoint { get; set; }=string.Empty;
        public DateTime Timestamp { get; set; }=string.Empty;
    }
}