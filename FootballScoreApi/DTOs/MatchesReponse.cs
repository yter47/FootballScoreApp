namespace FootballScoreApp.DTOs
{
    public class MatchesReponse
    {
        public ResultSet ResultSet { get; set; }
        public IEnumerable<Match> Matches { get; set; }
    }

    public class ResultSet
    {
        public int Count { get; set; }
        public string Competitions { get; set; }
        public DateOnly First { get; set; }
        public DateOnly Last { get; set; }
        public int Played { get; set; }
    }
}
