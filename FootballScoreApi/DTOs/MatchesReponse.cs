namespace FootballScoreApp.DTOs
{
    public class MatchesReponse
    {
        public Filters? Filters { get; set; }
        public ResultSet? ResultSet { get; set; }
        public Competiton? Competition { get; set; }
        public IEnumerable<Match> Matches { get; set; }
    }

    public class ResultSet
    {
        public int Count { get; set; }
        public DateOnly First { get; set; }
        public DateOnly Last { get; set; }
        public int Played { get; set; }
    }

    public class Filters
    {
        public string Season { get; set; }
    }
}
