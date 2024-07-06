using AirportLockerRental.UI.DTOs;

namespace AirportLockerRental.UI.Storage
{
    public interface ILockerRepository
    {
        public int Capacity { get; set; }
        void ListContents();
        LockerContents? GetLockerContents(int number);
        bool IsAvailable(int number);
        bool AddContents(LockerContents contents);
        LockerContents? RemoveContents(int number);
    }
}
