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
        }

        [Test]
        public void GetTripsByUser_WhenNotLoggedIn_ThrowsException()
        {
            MockTripService tripService = new MockTripService();
            _user = null;

            Assert.Throws<UserNotLoggedInException>(() => tripService.GetTripsByUser(null));
        }

        [Test]
        public void GetTripsByUser_WhenLoggedIn_ReturnsTripList()
        {
            MockTripService tripService = new MockTripService();
            User.User user = new User.User();

            var result = tripService.GetTripsByUser(user);

            Assert.NotNull(result);
            Assert.IsInstanceOf<List<Trip.Trip>>(result);
        }
    }
}
