namespace FootballScoreApp.Entities
{
    public class RefreshToken : BaseEntity
    {
        public string Token { get; set; }
        public DateTime? RefreshTokenExpiryTimeUtc { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
