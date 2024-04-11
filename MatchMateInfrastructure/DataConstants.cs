
namespace MatchMateInfrastructure
{
    public static class DataConstants
    {
        public const string DateTimeFormat = "dd/MM/yyyy hh:mm";

        public const string BirthdateFormat = "dd/MM/yyyy";

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

            public const int MaxPlaceLength = 70;
            public const int MinPlaceLength = 5;
        }

        public static class MessageConstants
        {
            public const int MaxContentLength = 300;
            public const int MinContentLength = 2;
        }

        public static class ApplicationUserDataConstants
        {
            public const int MaxBioLength = 100;
            public const int MinBioLength = 10;
        }
        public static class ReportsConstants
        {
            public const int CommentMaxLength = 100;
            
        }

        public static class ControllerWithPaginationNames
        {
            public const string InterestPanelController = "InterestPanel";
            public const string FriendshipController = "Friendship";
            public const string OfferController = "Offer";
            public const string BlockController = "Block";
            public const string ReportedOffersController = "ReportedOffers";
        }
        public static class ActionsWithPaginationNames
        {
            public const string PendingAction = "Pending";
            public const string IndexAction = "Index";
        }
        public static class CustomClaimsType
        {
            public const string HasBio = "user:hasbio";
            public const string HasInterests = "user:hasinterests";
            public const string HasPfp = "user:haspfp";
        }

    }
}
