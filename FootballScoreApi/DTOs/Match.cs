namespace FootballScoreApp.DTOs
{
    public class Match
    {
        public Area Area { get; set; }
        public League Competition { get; set; }
        public Season Season { get; set; }
        public DateTime UtcDate { get; set; }
        public string Status { get; set; }
        public int Matchday { get; set; }
        public string Stage { get; set; }
        public string? Group { get; set; }
        public DateTime LastUpdated { get; set; }
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
        public Score Score { get; set; }
        public List<Person> Referees { get; set; }
    }
}
