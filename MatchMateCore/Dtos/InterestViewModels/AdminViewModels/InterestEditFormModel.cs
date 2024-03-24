using System.ComponentModel.DataAnnotations;

namespace MatchMateCore.Dtos.InterestViewModels.AdminViewModels
{
    public class InterestEditFormModel : InterestPostFormModel
    {
        [Required]
        public int Id { get; set; }
    }
}
