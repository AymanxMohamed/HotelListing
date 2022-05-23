using HotelListing.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IList.Data
{
    public class DatabaseContext : IdentityDbContext<ApiUser>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Country>().HasData(
                new Country
                {
                    Id = 1,
                    Name = "Cairo",
                    ShortName = "CR"
                },
                new Country
                {
                    Id = 2,
                    Name = "Alex",
                    ShortName = "Ax"
                },
                new Country
                {
                    Id = 3,
                    Name = "Mansoura",
                    ShortName = "MNS"
                }
            );
            builder.Entity<Hotel>().HasData(
                new Hotel
                {
                    Id = 1,
                    Name = "ITI",
                    Address = "Giza",
                    CountryId = 1,
                    Rating = 3,
                },
                new Hotel
                {
                    Id = 2,
                    Name = "San Stifano",
                    Address = "Alxa",
                    CountryId = 1,
                    Rating = 5,
                },
                new Hotel
                {
                    Id = 3,
                    Name = "ITI3",
                    Address = "Gazx",
                    CountryId = 3,
                    Rating = 1,
                }
            );


        }
    }
}
