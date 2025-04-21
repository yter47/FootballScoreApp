using FootballScoreApp.Entities;

namespace FootballScoreApp.Providers
{
    public interface ITokenProvider
    {
        string CreateToken(User user);
        RefreshToken GenerateRefreshToken(User user);
    }
}
