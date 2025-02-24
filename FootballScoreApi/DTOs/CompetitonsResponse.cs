namespace FootballScoreApp.DTOs
{
    public class CompetitonsResponse
    {
        public int Count { get; set; }
        public IEnumerable<League> Competitions { get; set; }
    }
}
