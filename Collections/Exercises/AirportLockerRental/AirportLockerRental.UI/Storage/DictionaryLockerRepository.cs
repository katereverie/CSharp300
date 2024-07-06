using AirportLockerRental.UI.Actions;
using AirportLockerRental.UI.DTOs;

namespace AirportLockerRental.UI.Storage
{
    internal class DictionaryLockerRepository : ILockerRepository
    {
        public int Capacity { get; set; }
        public SortedDictionary<int, LockerContents> Lockers { get; set; }

        public DictionaryLockerRepository(int capacity)
        {
            Capacity = capacity;
            Lockers = new SortedDictionary<int, LockerContents>();
        }

        public bool AddContents(LockerContents contents)
        {
            if (Lockers.ContainsKey(contents.LockerNumber) || Lockers.Count >= Capacity)
            {
                return false;
            }
            
            Lockers.Add(contents.LockerNumber, contents);

            return true;

        }

        public LockerContents? GetLockerContents(int number)
        {
            if (Lockers.ContainsKey(number))
            {
                return Lockers[number];
            }

            return null;
        }

        public bool IsAvailable(int number)
        {
            if (Lockers.ContainsKey(number))
            {
                return false;
            }

            return true;
        }

        public void ListContents()
        {
            foreach (LockerContents contents in Lockers.Values)
            {
                ConsoleIO.DisplayLockerContents(contents);
            }
        }

        public LockerContents? RemoveContents(int number)
        {
            if (Lockers.ContainsKey(number))
            {
                var contentsToRemove = Lockers[number];
                Lockers.Remove(number);
                return contentsToRemove;
            }

            return null;
        }
    }
}
