using System.Globalization;

using MatchMateInfrastructure.Enums;
using static MatchMateInfrastructure.DataConstants;

namespace MatchMateCore.Dtos.OfferViewModels
{
    public class OfferDetailsModel : OfferPreviewModel
    {
        public OfferDetailsModel(int id, string title, OfferStatus offerStatus,
            string suggestedById, string suggestedByUsername,
            string receivedById, string receivedByUsername,
            string description, string place, DateTime time)
            :base(id,title, offerStatus, suggestedById,suggestedByUsername,receivedById,receivedByUsername)
        {
            Description = description;
            Place = place;
            Time = time.ToString(DateTimeFormat,CultureInfo.InvariantCulture);
        }
        public string Description { get; set; } = string.Empty;

        public string Place { get; set; } = string.Empty;

        public string Time { get; set; } = string.Empty;
    }
}
