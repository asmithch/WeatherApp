using AuthenticationService.Infrastructure.Data;
using AuthenticationService.Domain.Entities;

public class RefreshTokenService : IRefreshTokenService
{
    private readonly AppDbContext _context;

    public RefreshTokenService(AppDbContext context)
    {
        _context = context;
    }

    public string GenerateRefreshToken()
    {
        return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
    }

    public async Task SaveRefreshToken(int userId, string token)
    {
        var refreshToken = new RefreshToken
        {
            UserId = userId,
            Token = token,
            ExpiryDate = DateTime.UtcNow.AddDays(7)
        };

        _context.RefreshTokens.Add(refreshToken);
        await _context.SaveChangesAsync();
    }
}