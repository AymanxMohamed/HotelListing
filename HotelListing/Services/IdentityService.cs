using HotelListing.Data;
using IList.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace HotelListing.Services
{
    public static class CorsService
    {
        public static void AddAppIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentityCore<ApiUser>(x => x.User.RequireUniqueEmail = true);
            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), services);
            builder.AddEntityFrameworkStores<DatabaseContext>().AddDefaultTokenProviders();
        }
    }
}
