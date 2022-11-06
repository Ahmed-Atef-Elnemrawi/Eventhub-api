using EventHub.EventManagement.Application.Contracts.Persistance;
using EventHub.EventManagement.Persistance.Repositories;
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

      public static void ConfigureRepositoryManager(this IServiceCollection services) =>
         services.AddScoped<IRepositoryManager, RepositoryManager>();

   }
}
