namespace Domain.Services
{
    public interface IPasswordService
    {
        Task<bool> ValidatePasswordStrengthAsync(string password);
        Task<string> GenerateResetTokenAsync();
        Task<bool> VerifyResetTokenAsync(string token);
    }
}
