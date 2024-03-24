using MatchMateCore.Dtos.InterestViewModels.AdminViewModels;
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
        public Task<List<InterestGetModel>> GetAllInterestsAsync(InterestPanelList interestPanelModel)
        {
            var interests = _repository.AllReadOnly<Interest>()
            .OrderByDescending(i => i.UserInterest.Count);

            interestPanelModel.TotalInterestsCount = interests.Count();

            return interests
             .Skip((interestPanelModel.CurrentPage - 1) * InterestPanelList.CountOnPage)
            .Take(InterestPanelList.CountOnPage)
            .Select(i => new InterestGetModel()
            {
                Name = i.Name,
                PeopleCount = i.UserInterest.Count
            })
            .ToListAsync();
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
        public Task<bool> CheckIfThereIsAnInterestByNameAsync(string name) =>
            _repository.AllReadOnly<Interest>()
            .AnyAsync(i => i.Name.ToLower() == name);

        public async Task<bool> CheckIfThereAreAtLeastThreeInterestsAsync() =>
            _repository.AllReadOnly<Interest>()
            .Count() >= 3;

        private async Task<Interest?> GetInterestByIdAsync(int id) =>
           await _repository.All<Interest>().FirstOrDefaultAsync(i => i.Id == id);
    }
}
