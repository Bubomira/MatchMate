namespace MatchMateCore.Dtos.InterestViewModels
{
    public class InterestModel
    {
        public InterestModel(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public bool IsChecked { get; set; } = false;
    }
}
