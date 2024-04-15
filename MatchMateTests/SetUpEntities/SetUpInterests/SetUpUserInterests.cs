using MatchMateInfrastructure.Models;

using static MatchMateTests.SetUpEntities.SetUpInterests.SetUpInterests;
using static MatchMateTests.SetUpEntities.SetUpUsers;

namespace MatchMateTests.SetUpEntities.SetUpInterests
{
    public static class SetUpUserInterests
    {
        public static UserInterest FirstUserInterest =
               new UserInterest()
               {
                   UserId = FirstUser.Id,
                   User = FirstUser,
                   InterestId = FirstInterest.Id,
                   Interest = FirstInterest
               };

        public static UserInterest SecondUserInterest =
                new UserInterest()
                {
                    UserId = SecondUser.Id,
                    User = SecondUser,
                    InterestId = FirstInterest.Id,
                    Interest = FirstInterest
                };

        public static UserInterest ThirdUserInterest =
               new UserInterest()
               {
                   UserId = SecondUser.Id,
                   User = SecondUser,
                   InterestId = SecondInterest.Id,
                   Interest = SecondInterest
               };

        public static UserInterest FourthUserInterest =
               new UserInterest()
               {
                   UserId = ThirdUser.Id,
                   User = ThirdUser,
                   InterestId = SecondInterest.Id,
                   Interest = SecondInterest
               };

        public static UserInterest FifthUserInterest =
               new UserInterest()
               {
                   UserId = ThirdUser.Id,
                   User = ThirdUser,
                   InterestId = ThirdInterest.Id,
                   Interest = ThirdInterest
               };
    }
}
