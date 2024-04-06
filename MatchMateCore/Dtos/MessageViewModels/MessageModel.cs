
namespace MatchMateCore.Dtos.MessageViewModels
{
    public class MessageModel
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime DateSent { get; set; }
        public string SenderId { get; set; } = string.Empty;
        public string ReceiverId { get; set; } = string.Empty;
    }
}
