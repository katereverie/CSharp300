using AirportLockerRental.UI.Actions;
using AirportLockerRental.UI.DTOs;

namespace AirportLockerRental.UI.Storage
{
    public class ArrayLockerRepository : ILockerRepository
    {
        public int Capacity { get; set; }
        public LockerContents[] Lockers { get; set; }

        public ArrayLockerRepository(int capacity)
        {
            Capacity = capacity;
            Lockers = new LockerContents[capacity];
        }

        public bool AddContents(LockerContents contents)
        {
            if (IsAvailable(contents.LockerNumber))
            {
                Lockers[contents.LockerNumber - 1] = contents;
                return true;
            }

            return false;
        }

        public LockerContents? GetLockerContents(int number)
        {
            return Lockers[number - 1];
        }

        public bool IsAvailable(int number)
        {
            return Lockers[number - 1] == null;
        }

        public void ListContents()
        {
            for (int i = 0; i < Lockers.Length; i++)
            {
                if (Lockers[i] != null)
                {
                    ConsoleIO.DisplayLockerContents(Lockers[i]);
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
                LockerContents contents = Lockers[number - 1];
                Lockers[number - 1] = null;
                return contents;
            }
        }
    }
}
