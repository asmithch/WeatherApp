public interface IRefreshTokenService
{
    string GenerateRefreshToken();
    Task SaveRefreshToken(int userId, string token);
}