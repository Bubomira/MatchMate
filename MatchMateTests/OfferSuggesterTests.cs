using MatchMateCore.Dtos.OfferViewModels;
using MatchMateCore.Services.EntityServices.UserServices.OfferService;
using MatchMateInfrastructure.Data;
using MatchMateInfrastructure.Enums;
using MatchMateInfrastructure.Models;
using MatchMateInfrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;

namespace MatchMateTests
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
            var firstUser = new ApplicationUser() { Id = "1", UserName = "Mikael" };
            var secondUser = new ApplicationUser() { Id = "2", UserName = "Sanya" };
            var thirdUser = new ApplicationUser() { Id = "3", UserName = "Carl" };

            this._users = new List<ApplicationUser>()
            {
                firstUser,secondUser,thirdUser
            };

            this._offers = new List<Offer>()
            {
                new Offer() {Id=1,Description="Interesting",Place="mine",
                    Status=OfferStatus.Pending,Time=DateTime.Now,Title="Title",
                    ReceivingUser=firstUser,SuggestingUser=secondUser,SuggestingUserId="2",
                    ReceivingUserId="1"},

                 new Offer() {Id=2,Description="Boring",Place="cafe",
                    Status=OfferStatus.Cancelled,Time=DateTime.Now,Title="No title",
                    ReceivingUser=secondUser,SuggestingUser=thirdUser,SuggestingUserId="3",
                    ReceivingUserId="2"},

                  new Offer() {Id=3,Description="Exciting",Place="mall",
                    Status=OfferStatus.Accepted,Time=DateTime.Now,Title="Shopping",
                    ReceivingUser=secondUser,SuggestingUser=firstUser,SuggestingUserId="1",
                    ReceivingUserId="2"},

                   new Offer() {Id=4,Description="Astonishing",Place="chipolet",
                    Status=OfferStatus.Accepted,Time=DateTime.Now,Title="eating",
                    ReceivingUser=secondUser,SuggestingUser=firstUser,SuggestingUserId="1",
                    ReceivingUserId="2"},
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "MatchMate")
                .Options;

            this._context = new ApplicationDbContext(options);
            this._context.ApplicationUsers.AddRange(this._users);
            this._context.Offers.AddRange(this._offers);
            this._context.SaveChanges();

            this._repository = new Repository(_context);

            this._offerService = new OfferSuggesterService(_repository);
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
            var doesntExist = await _offerService.CheckIfOfferExists(4);

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
                ReceiverId = _users[1].Id,
                Title = "Interesting"
            };
            await _offerService.AddOfferAsync(offerModel, _users[2].Id, DateTime.Now);

            Assert.IsTrue(_repository.All<Offer>().Count() == 5);
        }

        [Test]
        public async Task ShouldSuccessfullyRemove()
        {

            await _offerService.DeleteOfferAsync(_offers[1].Id);

            Assert.IsTrue(_repository.All<Offer>().Count() == 3);
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

            var offers = await _offerService.GetOffersAsync(model, _users[1].Id);

            Assert.AreEqual(1,offers.Count);
            Assert.AreEqual(offers[0].Title, _offers[2].Title);
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

    }
}