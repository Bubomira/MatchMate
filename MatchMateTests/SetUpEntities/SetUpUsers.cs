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
            };

        public static ApplicationUser SecondUser =
            new ApplicationUser()
            {
                Id = "2",
                UserName = "Sanya",
            };

        public static ApplicationUser ThirdUser =
            new ApplicationUser()
            {
                Id = "3",
                UserName = "Carl",
            };
    }
}
