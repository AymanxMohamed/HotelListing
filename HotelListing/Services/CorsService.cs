using HotelListing.Models;
using IList.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace HotelListing.Services
{
    public static class IdentityService
    {
        public static void AddAppCors(this IServiceCollection services)
        {
            services.AddCors(x => x.AddPolicy("AllowAll", builder =>
                    builder.AllowAnyHeader()
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                )
            );
        }
    }
}
