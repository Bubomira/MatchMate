using MatchMateCore.Interfaces.EntityInterfaces.AdminInterfaces;
using MatchMateCore.Interfaces.EntityInterfaces.UserInterfaces;
using MatchMateCore.Interfaces.MongoInterfaces;
using MatchMateCore.Services.EntityServices.AdminServices;
using MatchMateCore.Services.EntityServices.UserServices;
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
            services.AddScoped<IFriendshipInterface, FriendshipService>();

            //Admin services
            services.AddScoped<IAdminInterestInterface, AdminInterestService>();
            return services;
        }
    }
}
