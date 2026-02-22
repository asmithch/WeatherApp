using AuthenticationService.Domain.Entities;

namespace AuthenticationService.Application.Services;

public interface IJwtService
{
    string GenerateToken(User user);
}