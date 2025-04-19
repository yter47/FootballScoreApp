using FootballScoreApp.DTOs;
using FootballScoreApp.Entities;

namespace FootballScoreApp.Services.IServices
{
    public interface ITokenService
    {
        string CreateToken(User user);
        RefreshToken GenerateRefreshToken(User user);
    }
}
