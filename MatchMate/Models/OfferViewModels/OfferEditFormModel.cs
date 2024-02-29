using System.ComponentModel.DataAnnotations;

namespace MatchMate.Models.OfferViewModels

{
    public class OfferEditFormModel :OfferPostFormModel
    {
        [Required]
        public int Id { get; set; }      
    }
}
