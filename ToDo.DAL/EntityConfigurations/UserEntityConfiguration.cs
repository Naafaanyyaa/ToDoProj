using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDo.DAL.Entities.Identity;

namespace ToDo.DAL.EntityConfigurations
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<UserDto>
    {
        public void Configure(EntityTypeBuilder<UserDto> builder)
        {
           builder.HasMany(ur => ur.UserRoles)
               .WithOne(u => u.UserDto)
               .HasForeignKey(x => x.UserId)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
