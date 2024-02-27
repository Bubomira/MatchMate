using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchMateInfrastructure
{
    public static class DataConstants
    {
        public static class InterestDataConstants
        {
            public const int MaxNameLength = 100;
            public const int MinNameLength = 3; 
        }

        public static class OfferConstants
        {
            public const int MaxTitleLength = 50;
            public const int MinTitleLength = 5;

            public const int MaxDescriptionLength = 300;
            public const int MinDescriptionLength = 10;
        }
    }
}
