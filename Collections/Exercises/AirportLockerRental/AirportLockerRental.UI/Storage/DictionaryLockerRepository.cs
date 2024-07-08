using AirportLockerRental.UI.Actions;
using AirportLockerRental.UI.DTOs;

namespace AirportLockerRental.UI.Storage
{
    public class DictionaryLockerRepository : ILockerRepository
    {
        private SortedDictionary<int, LockerContents> _lockers = new SortedDictionary<int, LockerContents>();

        public int Capacity { get; private set; }
        
        public DictionaryLockerRepository(int capacity)
        {
            Capacity = capacity;
        }

        public bool AddContents(LockerContents contents)
        {
            if (_lockers.ContainsKey(contents.LockerNumber) || _lockers.Count >= Capacity)
            {
                return false;
            }
            
            _lockers.Add(contents.LockerNumber, contents);

            return true;

        }

        public LockerContents? GetLockerContents(int number)
        {
            if (_lockers.ContainsKey(number))
            {
                return _lockers[number];
            }

            return null;
        }

        public bool IsAvailable(int number)
        {
            if (_lockers.ContainsKey(number))
            {
                return false;
            }

            return true;
        }

        public void ListContents()
        {
            foreach (LockerContents contents in _lockers.Values)
            {
                ConsoleIO.DisplayLockerContents(contents);
            }
        }

        public LockerContents? RemoveContents(int number)
        {
            if (_lockers.ContainsKey(number))
            {
                var contentsToRemove = _lockers[number];
                _lockers.Remove(number);
                return contentsToRemove;
            }

            return null;
        }
    }
}
