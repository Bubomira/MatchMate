using MatchMateCore.Dtos.MessageViewModels;

namespace MatchMateCore.Interfaces.EntityInterfaces.UserInterfaces
{
    public interface IMessageInterface
    {
        public Task GetConversation(ConversationModel conversationModel);
        public Task<List<MessageModel>> GetMesages(ConversationModel conversationModel);
        public Task AddMessage(MessagePostFormModel messagePostFormModel, string senderId);
        public Task RemoveMessage(int messageId, string senderId);
        public Task<bool> CheckIfUserCanRemoveMessage(int messageId, string senderId);
    }
}
