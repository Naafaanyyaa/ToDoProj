using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDo.DAL.Entities.Identity;

namespace ToDo.DAL.EntityConfigurations
{
    public class RoleEntityConfiguration : IEntityTypeConfiguration<RoleDto>
    {
        public void Configure(EntityTypeBuilder<RoleDto> builder)
        {
            builder.HasMany(r => r.UserRoles)
                .WithOne(ru => ru.RoleDto)
                .HasForeignKey(x => x.RoleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
