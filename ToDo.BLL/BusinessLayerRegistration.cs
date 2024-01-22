using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using ToDo.BLL.Infrastructure;
using ToDo.BLL.Interfaces;
using ToDo.BLL.Services;
using ToDo.DAL;

namespace ToDo.BLL
{
    public static class BusinessLayerRegistration
    {
        public static IServiceCollection AddBusinessLayer(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDataLayer(configuration);

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IRoleInitializer, RoleInitializer>();
            services.AddScoped<IRegistrationService, RegistrationService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<JwtHandler>();

            return services;
        }
    }
}
