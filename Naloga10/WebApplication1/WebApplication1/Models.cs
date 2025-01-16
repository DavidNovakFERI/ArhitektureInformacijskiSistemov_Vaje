using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1
{
    public class Team
    {
        public int TeamId { get; set; }
        public string Name { get; set; }
        public string Coach { get; set; }

       

    }

    public class Player
    {
        public int PlayerId { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public int TeamId { get; set; }

       

    }

    public class Match
    {
        public int MatchId { get; set; }
        public DateTime Date { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public string Score { get; set; }
    }

    public class League
    {
        public int LeagueId { get; set; }
        public string Name { get; set; }
        public string Season { get; set; }
    }

    public class Statistic
    {
        public int StatisticId { get; set; }
        public int PlayerId { get; set; }
        public int MatchId { get; set; }
        public int Goals { get; set; }
        public int Assists { get; set; }
    }
}
