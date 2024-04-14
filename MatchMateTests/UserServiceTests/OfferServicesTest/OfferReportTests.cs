using MatchMateCore.Dtos.OfferViewModels;
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
    public class OfferReportTests
    {
        private OfferReportService _offerReportService;
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

            _offerReportService = new OfferReportService(_repository);
        }

        [Test]
        public async Task CheckIfUserCanReportTheOffer()
        {
            var canBeReporter = await _offerReportService.CheckIfTheCurrentUserCanBeReporter(_users[0].Id, _offers[0].Id);
            var cannotBeReporter = await _offerReportService.CheckIfTheCurrentUserCanBeReporter(_users[1].Id, _offers[0].Id);

            Assert.IsTrue(canBeReporter);
            Assert.IsFalse(cannotBeReporter);
        }

        [Test]
        public async Task ShouldReportOfferSuccessfully()
        {
            var offerReport = new OfferReportPostModel()
            {
                Comment = "Hurful",
                ReasonForReport = ReasonForReport.Other
            };
            await _offerReportService.ReportOffer(_offers[0].Id, offerReport);

            var reportedOffer = await _repository.AllReadOnly<ReportedOffer>()
                .Include(ro=>ro.Offer)
                .FirstAsync(ro => ro.OfferId == _offers[0].Id);

            Assert.IsNotNull(reportedOffer);
            Assert.AreEqual(_offers[0].Title, reportedOffer.Offer.Title);
        }

        [OneTimeTearDown]
        public async Task TearDown()
        {
            await _context.Database.EnsureDeletedAsync();
            await _context.DisposeAsync();
        }
    }
}
