using NUnit.Framework;
using System.Collections.Generic;
using TripServiceKata.Exception;
using TripServiceKata.Trip;

namespace TripServiceKata.Tests
{
    [TestFixture]
    public class TripServiceTest
    {
        public static User.User _user = new User.User();

        public class MockTripService : TripService
        {
            public override User.User GetLoggedInUser()
            {
                return _user;
            }

            public override List<Trip.Trip> FindTrips(User.User user)
            {
                return user.Trips();
            }
        }

        [Test]
        public void GetTripsByUser_WhenNotLoggedIn_ThrowsException()
        {
            MockTripService tripService = new MockTripService();
            _user = null;

            Assert.Throws<UserNotLoggedInException>(() => tripService.GetTripsByUser(null));
        }

        [Test]
        public void GetTripsByUser_WhenLoggedInAndNotFriends_ReturnsEmptyTripList()
        {
            MockTripService tripService = new MockTripService();
            User.User user = new User.User();

            var result = tripService.GetTripsByUser(user);

            Assert.IsInstanceOf<List<Trip.Trip>>(result);
            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void GetTripsByUser_WhenLoggedInAndFriends_ReturnsTrips()
        {
            MockTripService tripService = new MockTripService();
            _user = new User.User();
            User.User usersFriend = new User.User();
            usersFriend.AddFriend(_user);
            usersFriend.AddTrip(new Trip.Trip());

            var result = tripService.GetTripsByUser(usersFriend);

            Assert.IsInstanceOf<List<Trip.Trip>>(result);
            Assert.AreNotEqual(0, result.Count);
        }
    }
}
