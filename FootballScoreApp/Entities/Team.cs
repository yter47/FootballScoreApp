﻿namespace FootballScoreApp.Entities
{
    public class Team : BaseEntity
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Tla { get; set; }
        public string Crest { get; set; }
        public string Address { get; set; }
        public string Website { get; set; }
        public int Founded { get; set; }
        public string ClubColors { get; set; }
        public string Venue { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
