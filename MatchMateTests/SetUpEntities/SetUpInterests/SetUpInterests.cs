using MatchMateInfrastructure.Models;

using static MatchMateTests.SetUpEntities.SetUpInterests.SetUpUserInterests;

namespace MatchMateTests.SetUpEntities.SetUpInterests
{
    public static class SetUpInterests
    {
        public static Interest FirstInterest =
            new Interest()
            {
                Id = 1,
                Name = "Anime",
               
            };

        public static Interest SecondInterest =
            new Interest()
            {
                Id = 2,
                Name = "Painting",
               
            };

        public static Interest ThirdInterest =
            new Interest()
            {
                Id = 3,
                Name = "Hiking",
              
            };
    }
}
