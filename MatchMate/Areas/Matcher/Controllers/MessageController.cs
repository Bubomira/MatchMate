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

        [HttpPost]
        [IgnoreAntiforgeryToken]
        [ProducesResponseType(200)]
        public async Task<IActionResult> AddToDb([FromBody] MessagePostFormModel messagePostModel)
        {
            if (!await _friendshipService.CheckIfThereIsAnActiveFriendshipBetweenUsersAsync(messagePostModel.SenderId,messagePostModel.ReceiverId))
            {
                return RedirectToAction("Index", "Friendship");
            }

            await _messageService.AddMessage(messagePostModel);

            return Ok();
        }
    }
}
