using Data.Entities;

namespace Business.Interfaces;

public interface IJwtAuthService
{
    string CreateToken(User user);
}