namespace FootballScoreApp.DTOs
{
    public class Season
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? CurrentMatchDay { get; set; }
        public Team Winner { get; set; }
    }
}
