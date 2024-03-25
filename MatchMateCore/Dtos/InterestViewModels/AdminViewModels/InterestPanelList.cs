
namespace MatchMateCore.Dtos.InterestViewModels.AdminViewModels
{
    public class InterestPanelList
    {
        public InterestPanelList(int pageNumber)
        {
           CurrentPage = pageNumber;
        }
        public const int CountOnPage = 10;
        public IList<InterestGetModel> Interests { get; set; } = new List<InterestGetModel>();
        public int TotalInterestsCount { get; set; } 
        public int CurrentPage { get; set; } = 1;
        public double TotalPagesCount { get; set; }
    }
}
