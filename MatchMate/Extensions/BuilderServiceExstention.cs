using MatchMateCore.Interfaces.EntityInterfaces.UserInterfaces;
using MatchMateCore.Interfaces.MongoInterfaces;
using MatchMateCore.Services.EntityServices.UserServices;
using MatchMateCore.Services.MongoServices;

 namespace Microsoft.Extensions.DependencyInjection
{
    public static class BuilderServiceExstention
    {
        public static IServiceCollection AttachServices(this IServiceCollection services)
        {
            services.AddSingleton<IProfilePictureInterface, ProfilePictureService>();

            services.AddScoped<IInterestInterface, InterestService>();
            return services;
        }
    }
}
