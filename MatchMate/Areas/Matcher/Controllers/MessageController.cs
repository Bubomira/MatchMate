using MatchMate.Hubs;
using MatchMateCore.Dtos.MessageViewModels;
using MatchMateCore.Interfaces.EntityInterfaces.UserInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace MatchMate.Areas.Matcher.Controllers
{
    public class MessageController : BaseUserController
    {
        private readonly IMessageInterface _messageService;
        private readonly IFriendshipInterface _friendshipService;
        public MessageController(IMessageInterface messageInterface,
            IFriendshipInterface friendshipService)
        {
            _messageService = messageInterface;
            _friendshipService = friendshipService;
        }
        public async Task<IActionResult> LoadConversation(string id)
        {
            if (!await _friendshipService.CheckIfThereIsAnActiveFriendshipBetweenUsersAsync(id, User.Id()))
            {
                return RedirectToAction("Index", "Friendship");
            }
            ConversationModel conversation = new ConversationModel()
            {
                ReceiverId = id,
                SenderId = User.Id()
            };

            await _messageService.GetConversation(conversation);

            return View(conversation);

        }

        public async Task<IActionResult> GetPreviousMessages(ConversationModel conversationModel)
        {
            if (!await _friendshipService.CheckIfThereIsAnActiveFriendshipBetweenUsersAsync(conversationModel.SenderId, conversationModel.ReceiverId))
            {
                return RedirectToAction("Index", "Friendship");
            }           

            var messages = await _messageService.GetMesages(conversationModel);

            return Ok(messages);

        }


    }
}
