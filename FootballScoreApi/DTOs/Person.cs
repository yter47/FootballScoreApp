using FootballScoreApp.Entities;

namespace FootballScoreApp.DTOs
{
    public class Person
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Nationality { get; set; }
    }

    public class Coach
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public Contract? Contract { get; set; }
    }

    public class Contract
    {
        public string Start { get; set; }
        public string Until { get; set; }
    }

    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string Nationality { get; set; }
    }
}
