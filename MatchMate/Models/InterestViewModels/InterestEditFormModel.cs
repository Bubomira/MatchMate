using MatchMate.Models.InterestViewModel;
using System.ComponentModel.DataAnnotations;

namespace MatchMate.Models.InterestViewModels
{
    public class InterestEditFormModel:InterestPostFormModel
    {
        [Required]
        public int Id { get; set; }
    }
}
