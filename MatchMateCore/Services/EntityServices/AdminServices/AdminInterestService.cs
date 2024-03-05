
using MatchMateCore.Dtos.InterestViewModels;
using MatchMateCore.Interfaces.EntityInterfaces.AdminInterfaces;
using MatchMateInfrastructure.Models;
using MatchMateInfrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace MatchMateCore.Services.EntityServices.AdminServices
{
    public class AdminInterestService : IAdminInterestInterface
    {
        private readonly IRepository _repository;
        public AdminInterestService(IRepository repository)
        {
            _repository = repository;
        }
        public async Task AddNewInterestAsync(InterestPostFormModel interestPostModel)
        {
            await _repository.AddAsync<Interest>(new Interest() { Name = interestPostModel.Name });

            await _repository.SaveChangesAsync();
        }

        public async Task DeleteInterestAsync(int interestId)
        {
            var interest = await GetInterestByIdAsync(interestId);

            await _repository.Remove<Interest>(interest);

            await _repository.SaveChangesAsync();
        }

        public async Task EditInterestAsync(InterestEditFormModel interestEditModel)
        {
            var interest = await GetInterestByIdAsync(interestEditModel.Id);
            interest.Name = interestEditModel.Name;

            await _repository.SaveChangesAsync();
        }

        private async Task<Interest?> GetInterestByIdAsync(int id) =>
           await _repository.All<Interest>().FirstOrDefaultAsync(i => i.Id == id);
    }
}
