using MatchMateCore.Interfaces.EntityInterfaces.AdminInterfaces;
using MatchMateCore.Interfaces.EntityInterfaces.UserInterfaces;
using MatchMateCore.Interfaces.EntityInterfaces.UserInterfaces.OfferInterfaces;
using MatchMateCore.Interfaces.EntityInterfaces.UserInterfaces.UserInterfaces;
using MatchMateCore.Interfaces.MongoInterfaces;
using MatchMateCore.Services.EntityServices.AdminServices;
using MatchMateCore.Services.EntityServices.UserServices;
using MatchMateCore.Services.EntityServices.UserServices.OfferService;
using MatchMateCore.Services.EntityServices.UserServices.UserService;
using MatchMateCore.Services.MongoServices;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class BuilderServiceExstention
    {
        public static IServiceCollection AttachServices(this IServiceCollection services)
        {
            services.AddSingleton<IProfilePictureInterface, ProfilePictureService>();

            //User services
            services.AddScoped<IInterestInterface, InterestService>();

            services.AddScoped<IUserInterface, UserService>();
            services.AddScoped<IUserBlockInterface, UserBlockService>();
            services.AddScoped<IFriendshipInterface, FriendshipService>();

            services.AddScoped<IOfferSuggesterInterface, OfferSuggesterService>();
            services.AddScoped<IOfferReceiverInterface, OfferReceiverService>();
            services.AddScoped<IReportOfferInterface,OfferReportService>();

            //Admin services
            services.AddScoped<IAdminInterestInterface, AdminInterestService>();
            services.AddScoped<IAdminReportOfferInterface, AdminReportService>();
            services.AddScoped<IAdminOffenderInterface, AdminOffenderService>();
            return services;
        }
    }
}
