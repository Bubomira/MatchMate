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
                   .Where(ui => userInterestIds.Contains(ui.InterestId))
                   .Skip(3 * pageCount)
                   .Take(3)
                   .Select(ui => new UserCardModel()
                   {
                       UserId = ui.UserId,
                       Bio = ui.User.Bio,
                       Username = ui.User.UserName,
                       Birthday = ui.User.Birthday,
                       Interests = ui.User.UsersInterests.Select(uui => uui.Interest.Name).ToList()
                   })
                   .ToListAsync();

        }
    }
}
