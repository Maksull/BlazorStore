namespace MyStore.Models.Services.EmailService
{
    public class EmailDto
    {
        public string To { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;

        public Order? Order { get; init; }
    }
}
