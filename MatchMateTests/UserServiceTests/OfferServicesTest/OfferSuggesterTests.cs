using MatchMateCore.Dtos.OfferViewModels;
using MatchMateCore.Services.EntityServices.UserServices.OfferService;
using MatchMateInfrastructure.Data;
using MatchMateInfrastructure.Enums;
using MatchMateInfrastructure.Models;
using MatchMateInfrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

using static MatchMateTests.SetUpEntities.SetUpUsers;
using static MatchMateTests.SetUpEntities.SetUpOffers;
using NUnit.Framework;

namespace MatchMateTests.UserServiceTests.OfferServicesTest
{
    [TestFixture]
    public class OfferSuggesterTests
    {
        private OfferSuggesterService _offerService;

        private ApplicationDbContext _context;
        private Repository _repository;

        private List<Offer> _offers;
        private List<ApplicationUser> _users;

        [OneTimeSetUp]
        public void Setup()
        {
            _users = new List<ApplicationUser>()
            {
                FirstUser,SecondUser,ThirdUser
            };

            _offers = new List<Offer>()
            {
                FirstOffer,SecondOffer,ThirdOffer,FourthOffer
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "MatchMate")
                .Options;

            _context = new ApplicationDbContext(options);
            _context.ApplicationUsers.AddRange(_users);
            _context.Offers.AddRange(_offers);
            _context.SaveChanges();

            _repository = new Repository(_context);

            _offerService = new OfferSuggesterService(_repository);
        }

        [Test]
        public async Task ShouldCheckIfUserIsOfferSuggesterCorrectly()
        {
            var isOwnerResult = await _offerService.CheckIfOfferIsSuggestedByUser(_offers[0].Id, _users[1].Id);
            var isNotOwnerResult = await _offerService.CheckIfOfferIsSuggestedByUser(_offers[1].Id, _users[1].Id);

            Assert.IsTrue(isOwnerResult);
            Assert.IsFalse(isNotOwnerResult);
        }

        [Test]
        public async Task ShouldSeeIfOfferExists()
        {
            var exists = await _offerService.CheckIfOfferExists(_offers[1].Id);
            var doesntExist = await _offerService.CheckIfOfferExists(34);

            Assert.IsFalse(doesntExist);
            Assert.IsTrue(exists);
        }

        [Test]
        public async Task ShouldReturnOfferDetails()
        {
            var offer = await _offerService.GetOfferDetailsAsync(_offers[1].Id);

            Assert.AreEqual(offer.Title, _offers[1].Title);
            Assert.AreEqual(offer.Place, _offers[1].Place);
            Assert.AreEqual(offer.Description, _offers[1].Description);
            Assert.AreEqual(offer.SuggestedBy.Username, _offers[1].SuggestingUser.UserName);
        }
        [Test]
        public async Task ShouldGetOfferEditableData()
        {
            var editableOfferData = await _offerService.GetOfferEditableDataAsync(_offers[2].Id);

            Assert.AreEqual(editableOfferData.Title, _offers[2].Title);
            Assert.AreEqual(editableOfferData.Description, _offers[2].Description);
            Assert.AreEqual(editableOfferData.ReceiverUsername, _offers[1].ReceivingUser.UserName);
        }

        [Test]
        public async Task ShouldAddOfferToDb()
        {
            var offerModel = new OfferPostFormModel()
            {
                Description = "New",
                Place = "mine",
                ReceiverId = _users[0].Id,
                Title = "Interesting"
            };
            await _offerService.AddOfferAsync(offerModel, _users[2].Id, DateTime.Now);

            Assert.IsTrue(_repository.All<Offer>().Count() == 5);
        }

        [Test]
        public async Task ShouldSuccessfullyRemove()
        {

            await _offerService.DeleteOfferAsync(_offers[1].Id);

            Assert.AreEqual(_repository.All<Offer>().Count(), 4);
        }

        [Test]
        public async Task ShouldReturnCorrectInformationAboutUserReceivedOffers()
        {
            var model = new OfferIndexModel()
            {
                IsOfferReceiver = IsOfferReceiver.Yes,
                OfferTimeType = TimeTypeOffer.Before,
                Status = OfferStatus.Pending,
            };

            var offers = await _offerService.GetOffersAsync(model, _users[0].Id);

            Assert.AreEqual(2, offers.Count);
            Assert.AreEqual(_offers[0].Title, offers[0].Title);
        }

        [Test]
        public async Task ShouldReturnCorrectInformationAboutUserSuggestedOffers()
        {
            var model = new OfferIndexModel()
            {
                IsOfferReceiver = IsOfferReceiver.No,
                OfferTimeType = TimeTypeOffer.Before,
                Status = OfferStatus.Pending
            };

            var offers = await _offerService.GetOffersAsync(model, _users[1].Id);

            Assert.AreEqual(1, offers.Count);
            Assert.AreEqual(offers[0].Title, _offers[0].Title);
        }

        [Test]
        public async Task ShouldExectuteSearchCorrectly()
        {
            var model = new OfferIndexModel()
            {
                IsOfferReceiver = IsOfferReceiver.DoesntMatter,
                OfferTimeType = TimeTypeOffer.Before,
                Status = OfferStatus.Accepted,
                SearchString = "eati"
            };

            var offers = await _offerService.GetOffersAsync(model, _users[1].Id);

            Assert.AreEqual(1, offers.Count);
        }

        [OneTimeTearDown]
        public async Task TearDown()
        {
            await _context.Database.EnsureDeletedAsync();
            await _context.DisposeAsync();
        }

    }
}