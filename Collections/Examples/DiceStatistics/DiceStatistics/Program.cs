SortedDictionary<int, int> results = new SortedDictionary<int, int>();

Random random = new Random();

int die1, die2, roll;

for (int i = 0; i < 100; i++)
{
    die1 = random.Next(1, 7);
    die2 = random.Next(1, 7);
    roll = die1 + die2;

    if (results.ContainsKey(roll))
    {
        results[roll]++;
    }
    else
    {
        {
            results.Add(roll, 1);
        }
    }
}

Console.WriteLine("Results");

foreach(var key in results.Keys)
{
    Console.WriteLine($"{key} - {results[key]} ({results[key]/100.00:p0})");
}
