namespace FootballScoreApp.DTOs
{
    public class Score
    {
        public string Winner { get; set; }
        public string Duration { get; set; }
        public Result FullTime { get; set; }
        public Result HalfTime { get; set; }
    }

    public class Result
    {
        public int Home { get; set; }
        public int Away { get; set; }
    }
}
