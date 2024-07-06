namespace RestaurantGuestGame
{
    public class PartyManager
    {
        Queue<Party> _waitList;

        public PartyManager()
        {
            _waitList = new Queue<Party>();
        }

        public void AddParty(Party p)
        {
            _waitList.Enqueue(p);
        }

        public Party CallParty()
        {
            if (_waitList.Count > 0)
            {
                return _waitList.Dequeue();
            }
            else
            {
                return null;
            }
        }

        public bool RemoveParty(string lastName)
        {
            Queue<Party> newWaitList = new Queue<Party>();

            bool isPartyFound = false;

            foreach(Party p in _waitList)
            {
                if (p.Name == lastName)
                {
                    isPartyFound = true;
                    continue;
                }
                else
                {
                    newWaitList.Enqueue(p);
                }
            }

            if (isPartyFound)
            {
                _waitList = newWaitList;
            }

            return isPartyFound;
        }

        public void ListAllParties()
        {
            if (_waitList.Count == 0)
            {
                Console.WriteLine("Wait list is empty.");
                return;
            }

            ConsoleIO.DisplayWaitList(_waitList);
        }

        public void ListNextParty()
        {
            if (_waitList.Count == 0)
            {
                Console.WriteLine("The wait list is empty.");
                return;
            }

            ConsoleIO.DisplayNextParty(_waitList.Peek());
        }
    }
}
