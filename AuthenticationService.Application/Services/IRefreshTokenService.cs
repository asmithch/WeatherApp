namespace AuthenticationService.Application.Services
{
    public interface IRefreshTokenService
    {
        string GenerateRefreshToken();
        Task SaveRefreshToken(int userId, string token);
    }
}