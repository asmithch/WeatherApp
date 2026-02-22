namespace LoggingService.Domain.Entities;

public class ErrorLog
{
    public int Id { get; set; }

    public string? Message { get; set; }

    public string? StackTrace { get; set; }

    public DateTime CreatedDate { get; set; }
}