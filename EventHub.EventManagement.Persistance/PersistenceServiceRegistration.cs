using EventHub.EventManagement.Application.Contracts.Persistance;
using EventHub.EventManagement.Domain.Entities;
using EventHub.EventManagement.Persistance.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventHub.EventManagement.Presistence
{
   public static class PersistenceServiceRegistration
   {
      public static void ConfigureSqlContext(this IServiceCollection services,
         IConfiguration configuration)
      {
         services.AddDbContext<RepositoryContext>(options
            => options.UseSqlServer(configuration.GetConnectionString("sqlConnection")));
      }

      public static void ConfigureIdentity(this IServiceCollection services)
      {
         var identityBuilder = services.AddIdentityCore<User>(opt =>
         {
            opt.Password.RequireDigit = true;
            opt.Password.RequireLowercase = false;
            opt.Password.RequireUppercase = false;
            opt.Password.RequireNonAlphanumeric = false;
            opt.Password.RequiredLength = 10;
            opt.User.RequireUniqueEmail = true;
         })
         .AddRoles<IdentityRole>()
         .AddEntityFrameworkStores<RepositoryContext>()
         .AddDefaultTokenProviders();

         //set token valid for two hours
         services.Configure<DataProtectionTokenProviderOptions>(config =>
         {
            config.TokenLifespan = TimeSpan.FromHours(2);
         });
      }

      public static void ConfigureRepositoryManager(this IServiceCollection services) =>
         services.AddScoped<IRepositoryManager, RepositoryManager>();

   }
}
