using MatchMateCore.Dtos.UsersViewModels;
using MatchMateCore.Interfaces.EntityInterfaces.UserInterfaces;
using MatchMateInfrastructure.Models;
using MatchMateInfrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace MatchMateCore.Services.EntityServices.UserServices
{
    public class UserService : IUserInterface
    {
        private readonly IRepository _repository;
        public UserService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserProfileModel?> GetCurrentUserInfo(string userId) =>
           await _repository.AllReadOnly<ApplicationUser>()
            .Where(au => au.Id == userId)
            .Select(au => new UserProfileModel()
            {
                Bio = au.Bio,
                UserId = au.Id,
                Username = au.UserName
            })
            .FirstOrDefaultAsync();


        public async Task AddUserBio(string userBio, string userId)
        {
            var user = await _repository.All<ApplicationUser>()
                .FirstOrDefaultAsync(au => au.Id == userId);

            user.Bio = userBio;

            await _repository.SaveChangesAsync();
        }
        public async Task<List<UserCardModel>> GetUsersWithTheSameInterests(string userId, int pageCount)
        {
            var userInterestIds = await _repository.AllReadOnly<UserInterest>()
                .Where(ui => ui.UserId == userId)
                .Select(ui => ui.InterestId)
                .ToListAsync();

            return await _repository.AllReadOnly<UserInterest>()
                   .Where(ui => userInterestIds.Contains(ui.InterestId) && ui.UserId != userId
                   && ui.User.ReceivedFriendships.All(rf=>rf.SenderId!=userId)
                   && ui.User.SendFriendships.All(rf => rf.ReceiverId != userId))
                   .GroupBy(ui => ui.UserId)
                   .Skip(3 * pageCount)
                   .Take(3)
                   .Select(ui => new UserCardModel()
                   {
                       UserId = ui.First().UserId,
                       Bio = ui.First().User.Bio,
                       Username = ui.First().User.UserName,
                       Interests = ui.First().User.UsersInterests.Select(uui => uui.Interest.Name).ToList()
                   })
                   .ToListAsync();
        }

        public Task<bool> CheckIfUserHasBio(string userId) =>
            _repository.AllReadOnly<ApplicationUser>()
            .AnyAsync(au => au.Id == userId && !string.IsNullOrEmpty(au.Bio));
        
    }
}
