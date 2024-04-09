﻿using MatchMateCore.Dtos.MessageViewModels;
using MatchMateCore.Interfaces.EntityInterfaces.UserInterfaces;
using MatchMateCore.Services.EntityServices.UserServices.UserService;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace MatchMate.Hubs
{
    public class ChatHub : Hub
    {
        private IMessageInterface _messageService;
        private IFriendshipInterface _friendshipService;

        public ChatHub()
        {
        }

        //to be improved 
        public async Task SendMessage(MessagePostFormModel messageModel)
        {
            _friendshipService = Context.GetHttpContext().RequestServices.GetRequiredService<IFriendshipInterface>();

            if (await _friendshipService.CheckIfThereIsAnActiveFriendshipBetweenUsersAsync(messageModel.SenderId, messageModel.ReceiverId))
            {
                _messageService = Context.GetHttpContext().RequestServices.GetRequiredService<IMessageInterface>();
                await _messageService.AddMessage(messageModel);


                await Clients.User(messageModel.ReceiverId).SendAsync("ReceiveMessage", new { Content = messageModel.Content, IsSender = false });
                await Clients.User(messageModel.SenderId).SendAsync("ReceiveMessage", new { Content = messageModel.Content, IsSender = true });
            }

        }


    }
}
