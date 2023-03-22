
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Identity.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                    new IdentityUserRole<string>
                    {
                        RoleId = "c384f717-525b-48e9-9294-00c8dfd2b712",
                        UserId = "56ac8339-dd38-48ee-8f79-d733ca64fdce"
                    },
                    new IdentityUserRole<string>
                    {
                        RoleId = "125d4295-d57d-400e-80d4-63478d8eda61",
                        UserId = "533c2146-8b0f-4b6d-83c3-9c6aeb9ba569"
                    }
                );
        }
    }
}
