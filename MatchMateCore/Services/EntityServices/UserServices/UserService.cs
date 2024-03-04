using MatchMateInfrastructure.Data;
using MatchMateCore.Dtos.UsersViewModels;
using MatchMateCore.Interfaces.EntityInterfaces.UserInterfaces;
using Microsoft.EntityFrameworkCore;

namespace MatchMateCore.Services.EntityServices.UserServices
{
    public class UserService : IUserInterface
    {
        private readonly ApplicationDbContext _context;
        public UserService(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }
        public async Task<List<UserCardModel>> GetUsersWithTheSameInterests(string userId, int pageCount)
        {
            var userInterestIds = await _context.UsersInterests.Where(ui => ui.UserId == userId)
                .Select(ui => ui.InterestId)
                .ToListAsync();

            return await _context.UsersInterests.Where(ui => userInterestIds.Contains(ui.InterestId))
                   .Select(ui => new UserCardModel()
                   {
                       UserId = ui.UserId,
                       Bio = ui.User.Bio,
                       Username = ui.User.UserName,
                       Birthday = ui.User.Birthday,
                       Interests = ui.User.UsersInterests.Select(uui => uui.Interest.Name).ToList()
                   })
                   .Skip(3 * pageCount)
                   .Take(3)
                   .ToListAsync();

        }
    }
}
