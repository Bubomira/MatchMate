
namespace MatchMateCore.Dtos
{
    public class PaginatorModel
    {
        public PaginatorModel(string controller,string action,int currentPage,double maxPagesCount)
        {
            ControllerName = controller;
            ActionName = action;
            CurrentPage = currentPage;
            NextPage = currentPage + 1;
            PrevoiusPage = currentPage - 1;
            MaxPagesTotal=maxPagesCount;
        }

        public string ControllerName { get; set; } = string.Empty;
        public string ActionName { get; set; } = string.Empty;
        public int NextPage { get; set; }
        public int CurrentPage { get; set; }
        public int PrevoiusPage { get; set; }
        public double MaxPagesTotal { get; set; }

    }
}
