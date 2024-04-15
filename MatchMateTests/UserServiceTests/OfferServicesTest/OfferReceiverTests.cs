
using MatchMateCore.Services.EntityServices.UserServices.OfferService;
using MatchMateInfrastructure.Data;
using MatchMateInfrastructure.Enums;
using MatchMateInfrastructure.Models;
using MatchMateInfrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using static MatchMateTests.SetUpEntities.SetUpOffers;
using static MatchMateTests.SetUpEntities.SetUpUsers;

namespace MatchMateTests.UserServiceTests.OfferServicesTest
{
    public class OfferReceiverTests
    {
        private OfferReceiverService _offerReceiverService;
        private ApplicationDbContext _context;
        private Repository _repository;

        private List<Offer> _offers;
        private List<ApplicationUser> _users;


        [OneTimeSetUp]
        public async Task SetUp()
        {
            _offers = new List<Offer>()
            {
                FirstOffer,SecondOffer,ThirdOffer,FourthOffer
            };
            _users = new List<ApplicationUser>()
            {
                FirstUser,SecondUser,ThirdUser
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "MatchMate")
            .Options;

            _context = new ApplicationDbContext(options);
            _context.ApplicationUsers.AddRange(_users);
            _context.Offers.AddRange(_offers);
            _context.SaveChanges();

            _repository = new Repository(_context);

            _offerReceiverService = new OfferReceiverService(_repository);
        }

        [Test]
        public async Task ShouldCorrectlyCheckIfAUserCanChangeOffersStatus()
        {
            var doesNotExist = await _offerReceiverService.CheckIfOfferStatusIsCorrectByStatusAsync
                (34, _users[0].Id, OfferStatus.Accepted);

            var existsWithIncorrectUserId = await _offerReceiverService.CheckIfOfferStatusIsCorrectByStatusAsync
                (_offers[0].Id, _users[2].Id, OfferStatus.Accepted);

            var existsWithIncorrectStatus = await _offerReceiverService.CheckIfOfferStatusIsCorrectByStatusAsync
               (_offers[0].Id, _users[0].Id, OfferStatus.Accepted);

            var existsAndIsModyfiable = await _offerReceiverService.CheckIfOfferStatusIsCorrectByStatusAsync
               (_offers[0].Id, _users[0].Id, OfferStatus.Pending);

            Assert.IsFalse(doesNotExist);
            Assert.IsFalse(existsWithIncorrectUserId);
            Assert.IsFalse(existsWithIncorrectStatus);
            Assert.IsTrue(existsAndIsModyfiable);
        }

        [Test]
        public async Task ShouldGetTheOffersReceiverUsername()
        {
            var receiverUsername = await _offerReceiverService.GetOfferReceiverUsernameAsync(_users[0].Id);

            Assert.AreEqual(_users[0].UserName, receiverUsername);
        }

        [Test]
        public async Task ShouldSuccessfullyAcceptOffer()
        {
            await _offerReceiverService.AcceptOfferAsync(_offers[0].Id);

            Assert.IsTrue(
               (await _repository.AllReadOnly<Offer>()
               .FirstAsync(o => o.Id == _offers[0].Id)).Status ==
                OfferStatus.Accepted);
        }

        [Test]
        public async Task ShouldSuccessfullyCancelOffer()
        {
            await _offerReceiverService.CancelOfferAsync(_offers[0].Id);

            Assert.IsTrue(
               (await _repository.AllReadOnly<Offer>()
               .FirstAsync(o => o.Id == _offers[0].Id)).Status ==
                OfferStatus.Cancelled);
        }

        [Test]
        public async Task ShouldSuccessfullyRejectOffer()
        {
            await _offerReceiverService.RejectOfferAsync(_offers[2].Id);

            Assert.AreEqual(4,await _repository.AllReadOnly<Offer>().CountAsync());
        }



        [OneTimeTearDown]
        public async Task TearDown()
        {
            await _context.Database.EnsureDeletedAsync();
            await _context.DisposeAsync();
        }

    }
}
