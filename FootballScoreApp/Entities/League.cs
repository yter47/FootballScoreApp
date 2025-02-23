namespace FootballScoreApp.Entities
{
    public class League : BaseEntity
    {
        public Area Area { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Type { get; set; }
        public string Emblem { get; set; }
        public string Plan { get; set; }
        public Season CurrentSeason { get; set; }
        public List<Season> Seasons { get; set; }
        public int NumberOfAvailableSeasons { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
