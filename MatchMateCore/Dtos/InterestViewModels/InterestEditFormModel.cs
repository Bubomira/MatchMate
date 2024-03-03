using System.ComponentModel.DataAnnotations;

namespace MatchMateCore.Dtos.InterestViewModels
{
    public class InterestEditFormModel : InterestPostFormModel
    {
        [Required]
        public int Id { get; set; }
    }
}
