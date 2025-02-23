namespace FootballScoreApp.Entities
{
    public class Season : BaseEntity
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? CurrentMatchDay { get; set; }
        public Team Winner { get; set; }
    }
}
