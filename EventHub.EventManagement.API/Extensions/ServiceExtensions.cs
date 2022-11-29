using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EventHub.EventManagement.API.Extensions
{
   public static class ServiceExtensions
   {
      public static void ConfigureCors(this IServiceCollection services)
      {
         services.AddCors(options =>
         {
            options.AddPolicy("CorsPolicy", builder =>
            {
               builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()
                .WithExposedHeaders("X-Pagination");
            });
         });
      }

      public static void ConfigureIISIntergration(this IServiceCollection services)
      {
         services.Configure<IISOptions>(options =>
         {

         });
      }


      public static void ConfiugerVersioningService(this IServiceCollection services)
      {
         services.AddApiVersioning(config =>
         {
            config.ReportApiVersions = true;
            config.AssumeDefaultVersionWhenUnspecified = true;
            config.DefaultApiVersion = new ApiVersion(1, 0);
         });
      }

      public static void AddCustomMediaTypes(this IServiceCollection services)
      {
         services.Configure<MvcOptions>(config =>
         {
            var systemTextJsonOutputFormatter = config.OutputFormatters
               .OfType<SystemTextJsonOutputFormatter>()?.FirstOrDefault();

            if (systemTextJsonOutputFormatter != null)
            {
               systemTextJsonOutputFormatter.SupportedMediaTypes
               .Add("application/vnd.api.hateoas+json");

               systemTextJsonOutputFormatter.SupportedMediaTypes
               .Add("application/vnd.api.apiroot+json");
            }

            var xmlOutputFormatter = config.OutputFormatters
               .OfType<XmlDataContractSerializerOutputFormatter>()?.FirstOrDefault();

            if (xmlOutputFormatter != null)
            {
               xmlOutputFormatter.SupportedMediaTypes
                  .Add("application/vnd.api.hateoas+xml");

               xmlOutputFormatter.SupportedMediaTypes
                  .Add("application/vnd.api.apiroot+xml");
            }
         });
      }

      public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
      {
         var jwtSettings = configuration.GetSection("JwtSettings");
         var secretKey = Environment.GetEnvironmentVariable("secret");

         services.AddAuthentication(opt =>
         {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
         }).AddJwtBearer(options =>
         {
            options.TokenValidationParameters = new TokenValidationParameters
            {
               ValidateIssuer = true,
               ValidateAudience = true,
               ValidateLifetime = true,
               ValidateIssuerSigningKey = true,

               ValidIssuer = jwtSettings["validIssuer"],
               ValidAudience = jwtSettings["validAudience"],
               IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!))


            };
         });

      }
   }
}
