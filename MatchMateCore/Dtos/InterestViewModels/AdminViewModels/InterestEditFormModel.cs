using System.ComponentModel.DataAnnotations;

namespace MatchMateCore.Dtos.InterestViewModels.AdminViewModels
{
    public class InterestEditFormModel : InterestPostFormModel
    {
        public InterestEditFormModel()
        {

        }
        public InterestEditFormModel(string name,int id, int currentPage)
        {
            Id = id;
            CurrentPage = currentPage;
            Name = name;
        }

        [Required]
        public int Id { get; set; }

        //for navigation purposes
        public int CurrentPage { get; set; }
    }
}
