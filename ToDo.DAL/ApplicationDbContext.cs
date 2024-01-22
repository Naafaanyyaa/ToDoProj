using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDo.DAL.Entities;
using ToDo.DAL.Entities.Identity;
using ToDo.DAL.EntityConfigurations;

namespace ToDo.DAL
{
    public class ApplicationDbContext : IdentityDbContext<UserDto, RoleDto, string, IdentityUserClaim<string>, UserRoleDto,
        IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public DbSet<TaskDto> Tasks { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new RoleEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new TaskEntityConfiguration());
        }
    }
}
