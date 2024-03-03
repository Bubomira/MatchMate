
using MatchMateCore.Dtos.InterestViewModels;
using MatchMateCore.Interfaces.EntityInterfaces.AdminInterfaces;
using MatchMateInfrastructure.Data;
using MatchMateInfrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace MatchMateCore.Services.EntityServices.AdminServices
{
    public class AdminInterestService : IAdminInterestInterface
    {
        private readonly ApplicationDbContext _context;
        public AdminInterestService(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }
        public async Task AddNewInterestAsync(InterestPostFormModel interestPostModel)
        {
            Interest interest = new Interest() { Name = interestPostModel.Name };

            await _context.Interests.AddAsync(interest);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteInterestAsync(int interestId)
        {
            var interest = await GetInterestByIdAsync(interestId);

            _context.Interests.Remove(interest);

            await _context.SaveChangesAsync();
        }

        public async Task EditInterestAsync(InterestEditFormModel interestEditModel)
        {
            var interest = await GetInterestByIdAsync(interestEditModel.Id);
            interest.Name = interestEditModel.Name;

            await _context.SaveChangesAsync();
        }

        private async Task<Interest?> GetInterestByIdAsync(int id) =>
           await _context.Interests.FirstOrDefaultAsync(i => i.Id == id);
    }
}
