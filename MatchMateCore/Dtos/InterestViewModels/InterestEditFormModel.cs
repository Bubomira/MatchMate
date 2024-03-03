using System.ComponentModel.DataAnnotations;

namespace MatchMateCore.Dtos.InterestViewModels
{
    internal class InterestEditFormModel : InterestPostFormModel
    {
        [Required]
        public int Id { get; set; }
    }
}
