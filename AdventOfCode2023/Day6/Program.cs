namespace Day6
{
  internal class Program
  {
    static void Main(string[] args)
    {
      var races = LoadRaces(_input).ToList();
      var result = races.Select(GetNumbersOfWayHowToWin).Aggregate(1, (a, b) => a * b);
      Console.WriteLine(result);
    }

    private static int GetNumbersOfWayHowToWin(Race race)
    {
      var res = Enumerable.Range(0, race.Time)
        .Select(i => CalculateRaceTime(i, race.Time))
        .Where(raceTime => raceTime > race.Distance)
        .Count();

      return res;
    }

    private static int CalculateRaceTime(int waitTime, int raceDuration)
    {
      if (waitTime <= 0) 
      {
        return 0;
      }

      if (raceDuration == waitTime) 
      {
        return 0;
      }

      return (raceDuration - waitTime) * waitTime;
    }

    private static IEnumerable<Race> LoadRaces(string input)
    {
      var lines = input.Split(Environment.NewLine).ToList();

      var times = lines[0].Split(" ").Where(item => !string.IsNullOrEmpty(item)).ToList();
      var distances = lines[1].Split(" ").Where(item => !string.IsNullOrEmpty(item)).ToList();

      for (int i = 1; i < times.Count; i++)
      {
        yield return new Race(int.Parse(times[i]), int.Parse(distances[i]));
      }
    }

    private const string _input = """
      Time:        46     82     84     79
      Distance:   347   1522   1406   1471
      """;

    private const string _testInput = """
      Time:      7  15   30
      Distance:  9  40  200
      """;
  }

  internal record Race(int Time, int Distance);
}