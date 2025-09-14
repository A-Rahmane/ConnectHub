namespace Domain.Services.Models
{
    public class ModerationResult
    {
        public bool IsApproved { get; set; }
        public string? Reason { get; set; }
        public double ConfidenceScore { get; set; }
    }
}
