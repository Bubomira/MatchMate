using MatchMateInfrastructure.Enums;
using MatchMateInfrastructure.Models;

using static MatchMateTests.SetUpEntities.SetUpUsers;

namespace MatchMateTests.SetUpEntities
{
    public static class SetUpOffers
    {
        public static Offer FirstOffer = new Offer()
        {
            Id = 1,
            Description = "Interesting",
            Place = "mine",
            Status = OfferStatus.Pending,
            Time = DateTime.Now,
            Title = "Title",
            ReceivingUser = FirstUser,
            SuggestingUser = SecondUser,
            SuggestingUserId = "2",
            ReceivingUserId = "1"
        };

        public static Offer SecondOffer = new Offer()
        {
            Id = 2,
            Description = "Boring",
            Place = "cafe",
            Status = OfferStatus.Cancelled,
            Time = DateTime.Now,
            Title = "No title",
            ReceivingUser = SecondUser,
            SuggestingUser = ThirdUser,
            SuggestingUserId = "3",
            ReceivingUserId = "2"
        };

        public static Offer ThirdOffer = new Offer()
        {
            Id = 3,
            Description = "Exciting",
            Place = "mall",
            Status = OfferStatus.Accepted,
            Time = DateTime.Now,
            Title = "Shopping",
            ReceivingUser = SecondUser,
            SuggestingUser = FirstUser,
            SuggestingUserId = "1",
            ReceivingUserId = "2"
        };

        public static Offer FourthOffer = new Offer()
        {
            Id = 4,
            Description = "Astonishing",
            Place = "chipolet",
            Status = OfferStatus.Accepted,
            Time = DateTime.Now,
            Title = "eating",
            ReceivingUser = SecondUser,
            SuggestingUser = FirstUser,
            SuggestingUserId = "1",
            ReceivingUserId = "2"
        };
    }
}
