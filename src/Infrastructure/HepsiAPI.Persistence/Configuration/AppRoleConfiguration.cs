using HepsiAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HepsiAPI.Persistence.Configuration
{
    public class AppRoleConfiguration : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            var superAdminRole = new AppRole { Id = Guid.NewGuid(), Name = "admin", NormalizedName = "ADMIN", ConcurrencyStamp = Guid.NewGuid().ToString() };
            var userRole = new AppRole { Id = Guid.NewGuid(), Name = "user", NormalizedName = "USER", ConcurrencyStamp = Guid.NewGuid().ToString() };

            builder.HasData(superAdminRole, userRole);
        }
    }
}
