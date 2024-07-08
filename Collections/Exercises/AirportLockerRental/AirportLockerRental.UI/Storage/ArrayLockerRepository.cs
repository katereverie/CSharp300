using AirportLockerRental.UI.Actions;
using AirportLockerRental.UI.DTOs;

namespace AirportLockerRental.UI.Storage
{
    public class ArrayLockerRepository : ILockerRepository
    {
        private LockerContents[] _lockers;

        public int Capacity { get; private set; }
        
        public ArrayLockerRepository(int capacity)
        {
            Capacity = capacity;
            _lockers = new LockerContents[capacity];
        }

        public bool AddContents(LockerContents contents)
        {
            if (IsAvailable(contents.LockerNumber))
            {
                _lockers[contents.LockerNumber - 1] = contents;
                return true;
            }

            return false;
        }

        public LockerContents? GetLockerContents(int number)
        {
            return _lockers[number - 1];
        }

        public bool IsAvailable(int number)
        {
            return _lockers[number - 1] == null;
        }

        public void ListContents()
        {
            for (int i = 0; i < _lockers.Length; i++)
            {
                if (_lockers[i] != null)
                {
                    ConsoleIO.DisplayLockerContents(_lockers[i]);
                }

            }
        }

        public LockerContents? RemoveContents(int number)
        {
            if (IsAvailable(number))
            {
                return null;
            }
            else
            {
                LockerContents contents = _lockers[number - 1];
                _lockers[number - 1] = null;
                return contents;
            }
        }
    }
}
