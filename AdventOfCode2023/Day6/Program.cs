namespace Day6
{
  internal class Program
  {
    static void Main()
    {
      var races = LoadRaces(_input).ToList();
      var result = races
        .Select(GetNumbersOfWayHowToWin)
        .Aggregate(1, (a, b) => a * b);

      Console.WriteLine("--== PART 1 ==--");
      Console.WriteLine(result);

      var timePart2 = string.Empty;
      var distancePart2 = string.Empty;

      foreach (var race in races)
      {
        timePart2 += race.Time;
        distancePart2 += race.Distance;
      }

      Console.WriteLine(timePart2);
      Console.WriteLine("--== PART 2 ==--");

      var res = GetNumbersOfWayHowToWin(new Race(long.Parse(timePart2), long.Parse(distancePart2)));

      Console.WriteLine(res);
    }

    private static int GetNumbersOfWayHowToWin(Race race)
    {
      var res = Enumerable.Range(0, (int)race.Time)
        .Select(i => CalculateRaceTime(i, race.Time))
        .Where(raceTime => raceTime > race.Distance)
        .Count();

      return res;
    }

    private static long CalculateRaceTime(long waitTime, long raceDuration)
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

  internal record Race(long Time, long Distance);
}