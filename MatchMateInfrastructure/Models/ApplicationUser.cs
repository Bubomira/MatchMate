using MatchMateInfrastructure.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatchMateInfrastructure.Models
{
    [Comment("The application user")]
    public class ApplicationUser:IdentityUser
    {
        [Required]
        [Comment("Birthday of a user who has to be over 16 years old")]
        public DateTime Birthday { get; set; }
        public IList<UserInterest> UsersInterests { get; set; } = new List<UserInterest>();

        [InverseProperty("Sender")]
        public IList<Friendship> SendFriendships { get; set; } = new List<Friendship>();

        [InverseProperty("Receiver")]
        public IList<Friendship> ReceivedFriendships { get; set; } = new List<Friendship>();

        [InverseProperty("SuggestingUser")]
        public IList<Offer> SuggestedOffers { get; set; } = new List<Offer>();

        [InverseProperty("ReceivingUser")]
        public IList<Offer> ReeceivedOffers { get; set; } = new List<Offer>();

        [InverseProperty("Receiver")]
        public IList<Message> ReceivedMessages { get; set; } = new List<Message>();

        [InverseProperty("Sender")]
        public IList<Message> SednedMessages { get; set; } = new List<Message>();
    }
}
