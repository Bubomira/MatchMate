﻿
namespace MatchMate.Models.UsersViewModels
{
    public class UserCardModel
    {
        public string UserId { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public string Bio { get; set; } = string.Empty;

        public int Age { get; set; }

        public IList<string> Interests { get; set; } = new List<string>();


    }
}
