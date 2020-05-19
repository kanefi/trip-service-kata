using System.Collections.Generic;
using TripServiceKata.Exception;
using TripServiceKata.User;

namespace TripServiceKata.Trip
{
    public class TripService
    {
        public List<Trip> GetTripsByUser(User.User user)
        {
            List<Trip> tripList = new List<Trip>();
            User.User loggedUser = GetLoggedInUser();
            bool isFriend = false;
            if (loggedUser != null)
            {
                foreach (User.User friend in user.GetFriends())
                {
                    if (friend.Equals(loggedUser))
                    {
                        isFriend = true;
                        break;
                    }
                }
                if (isFriend)
                {
                    tripList = FindTrips(user);
                }
                return tripList;
            }
            else
            {
                throw new UserNotLoggedInException();
            }
        }

        public virtual List<Trip> FindTrips(User.User user)
        {
            return TripDAO.FindTripsByUser(user);
        }

        public virtual User.User GetLoggedInUser()
        {
            return UserSession.GetInstance().GetLoggedUser();
        }

     
    }
}
