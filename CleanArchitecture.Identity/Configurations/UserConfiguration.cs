using CleanArchitecture.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Identity.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            PasswordHasher<ApplicationUser> hasher = new PasswordHasher<ApplicationUser>();
            builder.HasData(
                new ApplicationUser
                {
                    Id = "56ac8339-dd38-48ee-8f79-d733ca64fdce",
                    Email = "admin@localhost.com",
                    NormalizedEmail = "admin@localhost.com",
                    Name = "Kingmau",
                    LastName = "Mendez",
                    UserName = "MrNugget",
                    NormalizedUserName = "Kingmau",
                    PasswordHash = hasher.HashPassword(null, "admin123*$"),
                    EmailConfirmed = true
                },
                new ApplicationUser
                {
                    Id = "533c2146-8b0f-4b6d-83c3-9c6aeb9ba569",
                    Email = "user@localhost.com",
                    NormalizedEmail = "user@localhost.com",
                    Name = "Piloto",
                    LastName = "Piloto",
                    UserName = "Piloto123",
                    NormalizedUserName = "Piloto",
                    PasswordHash = hasher.HashPassword(null, "admin123*$"),
                    EmailConfirmed = true
                }
            );
        }
    }
}
