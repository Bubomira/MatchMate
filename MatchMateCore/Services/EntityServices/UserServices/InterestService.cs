using AutoMapper;
using MatchMate.Data;
using MatchMateCore.Dtos.InterestViewModels;
using MatchMateCore.Interfaces.EntityInterfaces.UserInterfaces;
using MatchMateInfrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace MatchMateCore.Services.EntityServices.UserServices
{
    public class InterestService : IInterestInterface
    {
        private readonly ApplicationDbContext _context;
        public InterestService(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }
        public async Task AddInterestToUserCollectionAsync(int interestId, string userId)
        {
            var userInterest = new UserInterest()
            {
                InterestId = interestId,
                UserId = userId
            };

            await _context.UsersInterests.AddAsync(userInterest);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> CheckIfInterestIsAttachedToUser(int interestId, string userId) =>
            await _context.UsersInterests
            .AnyAsync(ui => ui.UserId == userId && ui.InterestId == interestId);


        public Task<List<InterestModel>> GetAllInterestsAsync() =>
            _context.Interests.Select(i => new InterestModel()
            {
                Id = i.Id,
                Name = i.Name
            }).ToListAsync();


        public async Task RemoveInterestFromUserCollectionAsync(int interestId, string userId)
        {
            var userInterest = await _context.UsersInterests
                 .FirstOrDefaultAsync(ui => ui.InterestId == interestId && ui.UserId == userId);

            _context.UsersInterests.Remove(userInterest);

            await _context.SaveChangesAsync();
        }
    }
}
