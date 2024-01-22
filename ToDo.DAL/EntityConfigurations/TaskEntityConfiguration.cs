using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDo.DAL.Entities;

namespace ToDo.DAL.EntityConfigurations
{
    public class TaskEntityConfiguration : IEntityTypeConfiguration<TaskDto>
    {
        public void Configure(EntityTypeBuilder<TaskDto> builder)
        {
            builder.HasOne(x => x.UserDto)
                .WithMany(u => u.Tasks)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
