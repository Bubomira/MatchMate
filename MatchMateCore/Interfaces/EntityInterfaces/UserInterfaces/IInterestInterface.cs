﻿using MatchMateCore.Dtos.InterestViewModels;

namespace MatchMateCore.Interfaces.EntityInterfaces.UserInterfaces
{
    public interface IInterestInterface
    { 
        public Task AddInterestToUserCollectionAsync(int interestId, string userId);
        public Task RemoveInterestFromUserCollectionAsync(int interestId, string userId);
        public Task<bool> CheckIfInterestIsAttachedToUser(int interestId, string userId);
        public Task<bool> CheckIfUserHasAtLeastXInterests(string userId,int comparison);
        public Task<bool> CheckIfInterestExists(int interestId);
        public Task<List<InterestModel>> GetAllInterestsForCurrentUserAsync(string userId);


    }
}
