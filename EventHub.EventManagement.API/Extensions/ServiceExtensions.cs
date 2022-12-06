using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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

      public static void ConfigureSwagger(this IServiceCollection services)
      {
         services.AddSwaggerGen(opt =>
         {
            opt.SwaggerDoc("v1", new OpenApiInfo
            {
               Title = "EventHub API",
               Version = "v1",
               Description = "EventHub API by Ahmed Atef",
               Contact = new OpenApiContact
               {
                  Name = "Ahmed Atef",
                  Email = "ah.at.elnemrawi@gmail.com",
                  Url = new Uri("https://www.twitter.com/Ahmed_Elnemrawi")
               }
            });

            var xmlFile = $"{typeof(Presentation.AssemblyReference).Assembly.GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            opt.IncludeXmlComments(xmlPath);

            opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
               In = ParameterLocation.Header,
               Description = "Place to add JWT with Bearer",
               Name = "Authorization",
               Type = SecuritySchemeType.ApiKey,
               Scheme = "Bearer"
            });
            opt.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
               {
                  new OpenApiSecurityScheme
               {
                Reference = new OpenApiReference
                {
                   Type = ReferenceType.SecurityScheme,
                   Id = "Bearer",
                 },
                 Name = "Bearer",
                 },
                 new List<string>()
                 }
            });
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
