using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDo.DAL.Entities.Identity;
using ToDo.DAL.Interfaces;
using ToDo.DAL.Repositories;

namespace ToDo.DAL
{
    public static class DataLayerRegistration
    {
        public static IServiceCollection AddDataLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<UserDto, RoleDto>()
                .AddUserStore<UserStore<UserDto, RoleDto, ApplicationDbContext, string, IdentityUserClaim<string>, UserRoleDto,
                    IdentityUserLogin<string>, IdentityUserToken<string>, IdentityRoleClaim<string>>>()
                .AddRoleStore<RoleStore<RoleDto, ApplicationDbContext, string, UserRoleDto, IdentityRoleClaim<string>>>()
                .AddSignInManager<SignInManager<UserDto>>()
                .AddRoleManager<RoleManager<RoleDto>>()
                .AddUserManager<UserManager<UserDto>>();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ITaskDtoRepository, TaskDtoRepository>();

            return services;
        }
    }
}
