
using MatchMateCore.Services.EntityServices.UserServices;
using MatchMateInfrastructure.Data;
using MatchMateInfrastructure.Models;
using MatchMateInfrastructure.UnitOfWork;

using static MatchMateTests.SetUpEntities.SetUpUsers;
using static MatchMateTests.SetUpEntities.SetUpFriendships;
using Microsoft.EntityFrameworkCore;
using MatchMateCore.Dtos.UsersViewModels;

namespace MatchMateTests.UserServiceTests
{
    [TestFixture]
    public class FriendshipTests
    {
        private FriendshipService _friendshipService;

        private ApplicationDbContext _context;
        private Repository _repository;

        private List<ApplicationUser> _users;
        private List<Friendship> _friendships;

        [OneTimeSetUp]
        public async Task SetUp()
        {
            _users = new List<ApplicationUser>() { FirstUser, SecondUser, ThirdUser, FourthUser };
            _friendships = new List<Friendship>() { FirstFriendship, SecondFriendship, ThirdFriendship };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
         .UseInMemoryDatabase(databaseName: "MatchMate")
         .Options;

            _context = new ApplicationDbContext(options);
            _context.ApplicationUsers.AddRange(_users);
            _context.Friendships.AddRange(_friendships);
            _context.SaveChanges();

            _repository = new Repository(_context);

            _friendshipService = new FriendshipService(_repository);
        }

        [Test]
        public async Task ShouldCheckIfThereIsARelationShipBetweenUsersAsync()
        {
            var hasRelationship = await _friendshipService.CheckIfThereIsARelationShipBetweenUsersAsync(_users[0].Id, _users[1].Id);
            var alsoHasRelationship = await _friendshipService.CheckIfThereIsARelationShipBetweenUsersAsync(_users[0].Id, _users[2].Id);
            var doesNotHaveRelationship = await _friendshipService.CheckIfThereIsARelationShipBetweenUsersAsync(_users[0].Id, _users[3].Id);

            Assert.IsTrue(hasRelationship);
            Assert.IsTrue(alsoHasRelationship);
            Assert.IsFalse(doesNotHaveRelationship);
        }

        [Test]
        public async Task ShouldCheckIfThereIsAnActiveRelationShipBetweenUsersAsync()
        {
            var isActive = await _friendshipService.CheckIfThereIsAnActiveFriendshipBetweenUsersAsync(_users[0].Id, _users[1].Id);
            var isPending = await _friendshipService.CheckIfThereIsAnActiveFriendshipBetweenUsersAsync(_users[1].Id, _users[2].Id);
            var doesNotHaveRelationship = await _friendshipService.CheckIfThereIsAnActiveFriendshipBetweenUsersAsync(_users[0].Id, _users[3].Id);

            Assert.IsTrue(isActive);
            Assert.IsFalse(isPending);
            Assert.IsFalse(doesNotHaveRelationship);
        }

        [Test]
        public async Task ShouldSendAndRejectFriendshipRequest()
        {
            await _friendshipService.SendFriendRequestAsync(_users[3].Id, _users[2].Id);
            await _friendshipService.RejectFriendRequestAsync(_users[3].Id, _users[2].Id);

            var shouldNotHaveActiveRelationship = await _friendshipService.CheckIfThereIsARelationShipBetweenUsersAsync(_users[3].Id, _users[2].Id);

            Assert.IsFalse(shouldNotHaveActiveRelationship);
        }

        [Test]
        public async Task ShouldSendAndAcceptFriendshipRequest()
        {
            await _friendshipService.SendFriendRequestAsync(_users[3].Id, _users[1].Id);
            await _friendshipService.AcceptFriendRequestAsync(_users[3].Id, _users[1].Id);

            var shouldHaveRelationship = await _friendshipService.CheckIfThereIsAnActiveFriendshipBetweenUsersAsync(_users[3].Id, _users[1].Id);

            Assert.IsTrue(shouldHaveRelationship);
        }

        [Test]
        public async Task ShouldGetActiveFriendsForUserCorrectly()
        {
            UserFriendshipModelList friendshipList = new UserFriendshipModelList();
            await _friendshipService.GetActiveFriendsAsync(_users[0].Id, friendshipList);

            Assert.IsTrue(friendshipList.TotalFriends == 2);
            Assert.IsTrue(friendshipList.Friends[0].UserId == _users[1].Id);
            Assert.IsTrue(friendshipList.Friends[1].UserId == _users[2].Id);
        }

        [Test]
        public async Task ShouldGetActiveFriendsForUserWithSearchCorrectly()
        {
            UserFriendshipModelList friendshipList = new UserFriendshipModelList()
            {
                SearchItem = "l"
            };
            await _friendshipService.GetActiveFriendsAsync(_users[0].Id, friendshipList);

            Assert.AreEqual(_users[2].UserName, friendshipList.Friends[0].Username);
            Assert.AreEqual(1,friendshipList.Friends.Count);
        }

        [Test]
        public async Task ShouldGetPendingRequestsForUser()
        {
            var pendingList = await _friendshipService.GetPendingRequestsAsync(_users[1].Id, 0);

            Assert.AreEqual(1, pendingList.TotalFriends);
            Assert.AreEqual(_users[2].UserName, pendingList.Friends[0].Username);
        }

        [Test]
        public async Task ShouldRemoveFriendshipBetweenUsers()
        {
            await _friendshipService.RemoveFriendAsync(_users[0].Id, _users[2].Id);
            var shouldNotExist = await _friendshipService.CheckIfThereIsARelationShipBetweenUsersAsync(_users[0].Id, _users[2].Id);

            Assert.IsFalse(shouldNotExist);
        }

        [OneTimeTearDown]
        public async Task TearDown()
        {
            await _context.Database.EnsureDeletedAsync();
            await _context.DisposeAsync();
        }
    }
}
