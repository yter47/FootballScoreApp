namespace FootballScoreApp.DTOs
{
    public class Standing
    {
        public string Stage { get; set; }
        public string Type { get; set; }
        public string? Group { get; set; }
        public IEnumerable<TeamStanding> Table { get; set; }
    }

    public class StandingResponse
    {
        public Filters Filters { get; set; }
        public Area Area { get; set; }
        public Competiton Competition { get; set; }
        public Season Season { get; set; }
        public IEnumerable<Standing> Standings { get; set; }
    }

    public class TeamStanding
    {
        public int Position { get; set; }
        public Team Team { get; set; }
        public int PlayedGames { get; set; }
        public int? Form { get; set; }
        public int Won { get; set; }
        public int Draw { get; set; }
        public int Lost { get; set; }
        public int Points { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }
        public int GoalDifference { get; set; }
    }
}
