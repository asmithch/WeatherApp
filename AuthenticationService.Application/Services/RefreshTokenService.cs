using AuthenticationService.Application.Services;

public class RefreshTokenService : IRefreshTokenService
{
    public string GenerateRefreshToken()
    {
        return Guid.NewGuid().ToString();
    }

    public async Task SaveRefreshToken(int userId, string token)
    {
        await Task.CompletedTask;
    }
}