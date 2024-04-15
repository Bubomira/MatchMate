
using MatchMateCore.Services.EntityServices.UserServices;
using MatchMateInfrastructure.Data;
using MatchMateInfrastructure.Models;
using MatchMateInfrastructure.UnitOfWork;

using static MatchMateTests.SetUpEntities.SetUpUsers;
using static MatchMateTests.SetUpEntities.SetUpInterests.SetUpInterests;
using static MatchMateTests.SetUpEntities.SetUpInterests.SetUpUserInterests;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace MatchMateTests.UserServiceTests
{
    [TestFixture]
    public class InterestTests
    {
        private InterestService _interestService;

        private ApplicationDbContext _context;
        private Repository _repository;

        private List<UserInterest> _userInterests;
        private List<ApplicationUser> _users;
        private List<Interest> _interests;

        [OneTimeSetUp]
        public async Task SetUp()
        {
            _userInterests = new List<UserInterest>() {
                FirstUserInterest,SecondUserInterest,ThirdUserInterest,FourthUserInterest,FifthUserInterest
            };

            _users = new List<ApplicationUser>()
            {
                FirstUser,SecondUser,ThirdUser
            };

            _interests = new List<Interest>()
            {
                FirstInterest,SecondInterest,ThirdInterest
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
          .UseInMemoryDatabase(databaseName: "MatchMate")
          .Options;

            _context = new ApplicationDbContext(options);
            _context.ApplicationUsers.AddRange(_users);
            _context.Interests.AddRange(_interests);
            _context.UsersInterests.AddRange(_userInterests);
            _context.SaveChanges();

            _repository = new Repository(_context);

            _interestService = new InterestService(_repository);
        }

        [Test]
        public async Task ShouldCheckIfInterestIsAttachedToUser()
        {
            var isAttached = await _interestService.CheckIfInterestIsAttachedToUser(_interests[0].Id, _users[0].Id);
            var isNotAttached = await _interestService.CheckIfInterestIsAttachedToUser(_interests[0].Id, _users[2].Id);

            Assert.IsTrue(isAttached);
            Assert.IsFalse(isNotAttached);
        }

        [Test]
        public async Task ShouldCheckIfInterestExistsCorrectly()
        {
            var exists = await _interestService.CheckIfInterestExists(_interests[0].Id);
            var doesNotExists = await _interestService.CheckIfInterestExists(54);

            Assert.IsTrue(exists);
            Assert.IsFalse(doesNotExists);
        }

        [Test]
        public async Task ShouldCheckIfUserHasAtLeastXInterestsCorrectly()
        {
            var hasEnough = await _interestService.CheckIfUserHasAtLeastXInterests(_users[1].Id, 2);
            var doesNotHaveEnough = await _interestService.CheckIfUserHasAtLeastXInterests(_users[0].Id, 5);

            Assert.IsTrue(hasEnough);
            Assert.IsFalse(doesNotHaveEnough);
        }

        [Test]
        public async Task ShouldGetAllInterestsForUserAndIfTheyAreCheckedCorrectly()
        {
            var interests = await _interestService.GetAllInterestsForCurrentUserAsync(_users[1].Id);

            Assert.IsTrue(interests[0].IsChecked);
            Assert.IsTrue(interests[1].IsChecked);
            Assert.IsFalse(interests[2].IsChecked);
        }

        [Test]
        public async Task ShouldRemoveInterestFromUserCollection()
        {
            await _interestService.RemoveInterestFromUserCollectionAsync(_interests[2].Id, _users[2].Id);

            var shouldNotBeAttached = await _interestService.CheckIfInterestIsAttachedToUser(_interests[2].Id, _users[2].Id);

            Assert.IsFalse(shouldNotBeAttached);
        }

        [Test]
        public async Task ShouldSuccessfullyAddInterestToUserCollection()
        {
            await _interestService.AddInterestToUserCollectionAsync(_interests[2].Id, _users[1].Id);

            var shouldBeAttached = await _interestService.CheckIfInterestIsAttachedToUser(_interests[2].Id, _users[1].Id);

            Assert.IsTrue(shouldBeAttached);
        }

        [OneTimeTearDown]
        public async Task TearDown()
        {
            await _context.Database.EnsureDeletedAsync();
            await _context.DisposeAsync();
        }
    }
}
