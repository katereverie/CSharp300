namespace LINQPracticeMinis
{
    public class LINQPractice
    {
        private List<Luchador> _luchadores;

        public LINQPractice(List<Luchador> luchadores)
        {
            _luchadores = luchadores;
        }

        // Find all luchadores that haven't won a championship.
        public List<Luchador> GetLuchadoresWithoutChampionships()
        {
            var result = _luchadores.Where(l => l.Championships == 0).ToList();

            return result;
        }

        // Find all luchadores from Mexico, order them by their alias.
        public List<Luchador> GetMexicanLuchadores()
        {
            var result = _luchadores.Where(l => l is Luchador).OrderBy(l => l.Alias).ToList();

            return result;

        }

        // Find the luchador with the most wins.
        public Luchador GetLuchadorWithMostWins()
        {
            var result = _luchadores.OrderByDescending(l => l.Wins).First();

            return result;
        }

        // Find the average weight of all luchadores.
        public decimal GetAverageWeight()
        {
            var result = _luchadores.Average(l => l.WeightKg);

            return result;
        }

        // Find the total number of wins by luchadores from the USA.
        public decimal GetTotalWinsByUSALuchadores()
        {
            var result = _luchadores.Where(l => l.Country == "USA").Sum(l => l.Wins);

            return result;
        }

        // Find the youngest luchador.
        public Luchador GetYoungestLuchador()
        {

            var result = _luchadores.OrderByDescending(l => l.DoB).First();

            return result;
        }

        // Find all luchadores with a height above 180 cm who have won a championship.
        public List<Luchador> GetTallChampionLuchadores()
        {
            var result = _luchadores.Where(l => l.HeightCm > 180).Where(l => l.Championships > 0).ToList();

            return result;
        }

        // Find the luchadors who have more than the average number of draws.
        public List<Luchador> GetLuchadorsMoreAverageDraws()
        {
            var avgDraws = _luchadores.Average(l => l.Draws);

            var result = _luchadores.Where(l => l.Draws > avgDraws).ToList();

            return result;
        }

        // Find the total number of championships won by Spanish luchadores.
        public int GetTotalChampionshipsBySpanishLuchadores()
        {
            var result = _luchadores.Where(l => l.Country == "Spain").Sum(l => l.Championships);

            return result;
        }

        // Find the oldest luchador from Mexico.
        public Luchador GetOldestMexicanLuchador()
        {
            var result = _luchadores.Where(l => l.Country == "Mexico").OrderBy(l => l.DoB).First();

            return result;
        }

        // Find all luchadores with more than 20 wins and less than 5 losses.
        public List<Luchador> GetSuccessfulLuchadores()
        {
            var result = _luchadores.Where(l => l.Wins > 20 && l.Losses < 5).ToList();

            return result;
        }

        // Find the alias of the luchador with the lowest win to loss ratio.
        public string GetLuchadorWithLowestWinToLossRatio()
        {
            var lowest = _luchadores.Min(l => l.Wins / l.Losses);

            var result = _luchadores.Where(l => l.Wins / l.Losses == lowest).First().Alias;

            return result;
        }

        // Find the average number of wins by country (round to 2 decimals).
        public Dictionary<string, decimal> GetAverageWinsByCountry()
        {
            Dictionary<string, decimal> result = new Dictionary<string, decimal>();

            var avgUSWins = _luchadores.Where(l => l.Country == "USA").Average(l => l.Wins);
            var avgMexicoWins = _luchadores.Where(l => l.Country == "Mexico").Average(l => l.Wins);
            var avgSpainWins = _luchadores.Where(l => l.Country == "Spain").Average(l => l.Wins);

            result.Add("USA", Math.Round(avgUSWins, 2));
            result.Add("Mexico", Math.Round(avgMexicoWins, 2));
            result.Add("Spain", Math.Round(avgSpainWins, 2));

            return result;
        }

        // Find all luchadores whose alias starts with the word "The".
        public List<Luchador> GetLuchadoresWithTheInAlias()
        {
            var result = _luchadores.Where(l => l.Alias.StartsWith("The")).ToList();

            return result;
        }

        // Find the luchador with the most losses who has won at least one championship.
        public Luchador GetLuchadorWithMostLossesAndAChampionship()
        {
            var result = _luchadores.Where(l => l.Championships >= 1).OrderByDescending(l => l.Losses).First();

            return result;
        }

        // Find the total number of draws by luchadores who have not won any championships.
        public decimal GetTotalDrawsByLuchadoresWithoutChampionships()
        {
            var result = _luchadores.Where(l => l.Championships == 0).Sum(l => l.Draws);

            return result;
        }

        // Find all luchadores who were born in the 1990s.
        public List<Luchador> GetLuchadoresBornInThe90s()
        {
            var startDate = new DateTime(1990, 1, 1);
            var endDate = new DateTime(2000, 1, 1);
            var result = _luchadores.Where(l => l.DoB >= startDate && l.DoB < endDate).ToList();

            return result;
        }

        // Find the luchador alias with the most matches (wins, losses, draws).
        public string GetLuchadorWithMostMatches()
        {
            var result = _luchadores.OrderByDescending(l => l.Wins + l.Losses + l.Draws).First().Alias;

            return result;
        }

        // Find the country with the most luchadores.
        public string GetCountryWithMostLuchadores()
        {
            var result = _luchadores.GroupBy(l => l.Country).OrderByDescending(group => group.Count()).First().Key;

            return result;
        }

        // Find the luchador with the longest alias name.
        public Luchador GetLuchadorWithLongestAlias()
        {
            var result = _luchadores.OrderByDescending(l => l.Alias.Length).First();

            return result;
        }
    }
}
