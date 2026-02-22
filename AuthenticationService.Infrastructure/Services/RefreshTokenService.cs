using AuthenticationService.Application.Services;

namespace AuthenticationService.Infrastructure.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        public string GenerateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }

        public async Task SaveRefreshToken(int userId, string token)
        {
            // Save to DB logic here
            await Task.CompletedTask;
        }
    }
}