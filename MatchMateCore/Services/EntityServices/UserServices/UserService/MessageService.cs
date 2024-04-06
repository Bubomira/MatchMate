

using MatchMateCore.Dtos.MessageViewModels;
using MatchMateCore.Interfaces.EntityInterfaces.UserInterfaces;
using MatchMateInfrastructure.Models;
using MatchMateInfrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using ZstdSharp.Unsafe;

namespace MatchMateCore.Services.EntityServices.UserServices.UserService
{
    public class MessageService : IMessageInterface
    {
        private readonly IRepository _repository;
        public MessageService(IRepository repository)
        {
            _repository = repository;
        }
        public async Task AddMessage(MessagePostFormModel messagePostFormModel, string senderId)
        {
            Message message = new Message()
            {
                SenderId = senderId,
                DateSend = DateTime.Now,
                Content = messagePostFormModel.Content,
                ReceiverId = messagePostFormModel.ReceiverId
            };

            await _repository.AddAsync<Message>(message);

            await _repository.SaveChangesAsync();
        }

        public async Task GetConversation(ConversationModel conversationModel)
        {
            var messages = _repository.AllReadOnly<Message>()
             .Where(m => m.SenderId == conversationModel.SenderId && m.ReceiverId == conversationModel.ReceiverId)
             .OrderByDescending(m => m.DateSend);

            conversationModel.MessagesTotal = await messages.CountAsync();

            conversationModel.Messages = await messages
                 .Take(ConversationModel.messagesOnInit)
                 .Select(m => new MessageModel()
                 {
                     Content = m.Content,
                     DateSent = m.DateSend,
                     Id = m.Id,
                     ReceiverId = m.ReceiverId,
                     SenderId = m.SenderId
                 })
                 .ToListAsync();
        }

        public Task<List<MessageModel>> GetMesages(ConversationModel conversationModel) =>
            _repository.AllReadOnly<Message>()
             .Where(m => m.SenderId == conversationModel.SenderId && m.ReceiverId == conversationModel.ReceiverId)
             .OrderByDescending(m => m.DateSend)
            .Skip((conversationModel.ScrollerCount * ConversationModel.messagesOnScroll) + 10)
            .Take(ConversationModel.messagesOnScroll)
             .Select(m => new MessageModel()
             {
                 Content = m.Content,
                 DateSent = m.DateSend,
                 Id = m.Id,
                 ReceiverId = m.ReceiverId,
                 SenderId = m.SenderId
             })
             .ToListAsync();


        public async Task RemoveMessage(int messageId, string senderId)
        {
            var message = await _repository.AllReadOnly<Message>()
              .FirstAsync(m => m.SenderId == senderId && m.Id == messageId);

            await _repository.Remove<Message>(message);

            await _repository.SaveChangesAsync();
        }
        public Task<bool> CheckIfUserCanRemoveMessage(int messageId, string senderId) =>
            _repository.AllReadOnly<Message>()
            .AnyAsync(m => m.SenderId == senderId && m.Id == messageId);

    }
}
