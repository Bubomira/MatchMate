using System.ComponentModel.DataAnnotations;

namespace MatchMateCore.Dtos.OfferViewModels
{
    public class OfferEditFormModel : OfferPostFormModel
    {
        [Required]
        public int Id { get; set; }
    }
}
