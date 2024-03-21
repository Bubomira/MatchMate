using MatchMateCore.Dtos.UsersViewModels;
using MatchMateInfrastructure.Enums;


namespace MatchMateCore.Dtos.OfferViewModels
{

    public class OfferPreviewModel
    {
        public OfferPreviewModel(int id, string title, OfferStatus offerStatus,
            string suggestedById,string suggestedByUsername,
            string receivedById, string receivedByUsername)
        {
            Id = id;
            Title = title;
            OfferStatus = offerStatus;
            SuggestedBy = new UserOfferModel(suggestedById, suggestedByUsername);
            ReceivedBy = new UserOfferModel(receivedById, receivedByUsername);
        }

        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public OfferStatus OfferStatus { get; set; }

        public UserOfferModel SuggestedBy { get; set; } = null!;

        public UserOfferModel ReceivedBy { get; set; } = null!;
    }
}
