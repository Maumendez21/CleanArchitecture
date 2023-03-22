using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Identity.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                    new IdentityRole
                    {
                        Id = "c384f717-525b-48e9-9294-00c8dfd2b712",
                        Name = "Administrator",
                        NormalizedName = "ADMINISTARDOR"
                    },
                    new IdentityRole
                    {
                        Id = "125d4295-d57d-400e-80d4-63478d8eda61",
                        Name = "Operator",
                        NormalizedName = "OPERATOR"
                    }
                );
        }
    }
}
