using Domain.Enums;

namespace Domain.Services.Models
{
    public class FollowResult
    {
        public bool IsSuccessful { get; set; }
        public FollowStatus Status { get; set; }
        public string? Message { get; set; }
    }
}
