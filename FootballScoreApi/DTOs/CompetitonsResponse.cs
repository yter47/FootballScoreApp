namespace FootballScoreApp.DTOs
{
    public class CompetitonsResponse
    {
        public int Count { get; set; }
        public IEnumerable<Competiton> Competitions { get; set; }
    }
}
