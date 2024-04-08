using MatchMateCore.Dtos.MessageViewModels;
using MatchMateCore.Interfaces.EntityInterfaces.UserInterfaces;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace MatchMate.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IMessageInterface _messageService;

        public ChatHub()
        {
        }

        //to be improved 
        public async Task SendMessage(MessageModel messageModel)
        {          
            await Clients.All.SendAsync("ReceiveMessage", new {Content=messageModel.Content,IsSender=messageModel.SenderId==Context?.User?.Id()});
        }


    }
}
