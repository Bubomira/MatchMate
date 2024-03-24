
namespace MatchMateCore.Dtos.InterestViewModels.AdminViewModels
{
    public class InterestPanelList
    {
        public InterestPanelList(int pageNumber)
        {

           CurrentPage = pageNumber;
           PreviousPage = pageNumber - 1;
           NextPage = pageNumber + 1;
        }
        public const int CountOnPage = 10;
        public IList<InterestGetModel> Interests { get; set; } = new List<InterestGetModel>();
        public int TotalInterestsCount { get; set; } 
        public int CurrentPage { get; set; } = 1;
        public int NextPage { get; set; }
        public int PreviousPage { get; set; }
        public double TotalPagesCount { get; set; }
    }
}
