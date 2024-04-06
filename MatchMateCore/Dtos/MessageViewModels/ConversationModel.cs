

namespace MatchMateCore.Dtos.MessageViewModels
{
    public class ConversationModel
    {
        public const int messagesOnScroll = 10; 
        public const int messagesOnInit = 20; 
        public string ReceiverId { get; set; } = string.Empty;
        public string ReceiverUsername { get; set; } = string.Empty;
        public string SenderId { get; set; } = string.Empty;
        public IList<MessageModel> Messages { get; set; } = new List<MessageModel>();
        public int MessagesTotal { get; set; }
        public int ScrollerCount { get; set; }
    }
}
