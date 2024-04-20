
using MatchMateInfrastructure.Models;

using static MatchMateTests.SetUpEntities.SetUpUsers;

namespace MatchMateTests.SetUpEntities
{
    public static class SetUpFriendships
    {
        public static Friendship FirstFriendship =
            new Friendship()
            {
                Id = 1,
                IsActive = true,
                Receiver = FirstUser,
                ReceiverId = FirstUser.Id,
                Sender = SecondUser,
                SenderId = SecondUser.Id,
            };

        public static Friendship SecondFriendship =
          new Friendship()
          {
              Id = 2,
              IsActive = true,
              Receiver = ThirdUser,
              ReceiverId = ThirdUser.Id,
              Sender = FirstUser,
              SenderId = FirstUser.Id,
          };

        public static Friendship ThirdFriendship =
         new Friendship()
         {
             Id = 3,
             IsActive = false,
             Receiver = SecondUser,
             ReceiverId = SecondUser.Id,
             Sender = ThirdUser,
             SenderId = ThirdUser.Id,
         };

    }
}
