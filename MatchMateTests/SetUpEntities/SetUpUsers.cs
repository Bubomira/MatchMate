using MatchMateInfrastructure.Models;

using static MatchMateTests.SetUpEntities.SetUpInterests.SetUpUserInterests;

namespace MatchMateTests.SetUpEntities
{
    public static class SetUpUsers
    {
        public static ApplicationUser FirstUser =
            new ApplicationUser()
            {
                Id = "1",
                UserName = "Mikael",
                Bio = "I am Mikael"
            };

        public static ApplicationUser SecondUser =
            new ApplicationUser()
            {
                Id = "2",
                UserName = "Sanya",
                Bio = "Passionate traveller"
            };

        public static ApplicationUser ThirdUser =
            new ApplicationUser()
            {
                Id = "3",
                UserName = "Carl",
            };

        public static ApplicationUser FourthUser =
          new ApplicationUser()
          {
              Id = "4",
              UserName = "Mariya",
          };


    }
}
