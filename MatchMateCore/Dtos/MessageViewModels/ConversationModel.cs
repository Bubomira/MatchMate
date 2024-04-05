

namespace MatchMateCore.Dtos.MessageViewModels
{
    public class ConversationModel
    {
        public const int messagesOnScroll = 10;
        public string ReceiverUsername { get; set; } = string.Empty;    
        public string ReceiverId { get; set; } = string.Empty;
        public string SenderId { get; set; } = string.Empty;
        public IList<MessageModel> Messages { get; set; } = new List<MessageModel>();
    }
}
