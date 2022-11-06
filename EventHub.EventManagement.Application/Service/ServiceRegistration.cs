using EventHub.EventManagement.Application.Contracts.Service;
using EventHub.EventManagement.Application.Contracts.Service.DataShaperService;
using EventHub.EventManagement.Application.Service.DataShaper;
using Microsoft.Extensions.DependencyInjection;

namespace EventHub.EventManagement.Application.Service
{
   public static class ServiceRegistration
   {
      public static void ConfigureServiceManager(this IServiceCollection service) =>
         service.AddScoped<IServiceManager, ServiceManager>();

      public static void ConfigureDataShaperServiceManager(this IServiceCollection services) =>
         services.AddScoped<IDataShaperManager, DataShaperManager>();
   }
}
