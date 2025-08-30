using FootballScoreApp.DTOs;

namespace FootballScoreApp.Entities
{
    public class UserTeam
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int TeamId { get; set; }
        public virtual Team Team { get; set; }
    }
}
