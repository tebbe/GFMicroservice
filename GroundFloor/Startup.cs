using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLayer;
using DatabaseLayer.Dal;
using DatabaseLayer.DBContext;
using HealthChecks.UI.Client;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Model;
using Model.DBModel.MongoModel;
using PremiseGlobalLibrary;
using PremiseGlobalLibrary.Middleware;

namespace GroundFloor
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            var jwtSection = Configuration.GetSection("JWT");
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {

                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidAudience = jwtSection.GetValue<string>("ValidAudience"),
                        ValidIssuer = jwtSection.GetValue<string>("ValidIssuer"),
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection.GetValue<string>("Secret")))
                    };
                });
            services.AddHealthChecks()
       //.AddRedis("172.31.2.141:6379,172.31.26.37:6379,172.31.42.80:6379", tags: new string[] { "Database" })
       .AddMongoDb(Configuration.GetSection("MongoDBSettings").GetValue<string>("ConnectionString"), name: "mongodb", tags: new string[] { "Database" });


            services.Configure<MongoDBSettings>(Configuration.GetSection("MongoDBSettings"));
            services.Configure<DBCollections>(Configuration.GetSection("DBCollections"));

          
            services.AddPremiseService(config =>
            {
                config.AppName = Configuration.GetValue<string>("AppName");
                config.APIDomain = Configuration.GetValue<string>("APIDomain");
                config.APIDomain = Configuration.GetValue<string>("APIDomain");
                config.RedisConnectionString = Configuration.GetConnectionString("Redis");
                config.ConnectionString = Configuration.GetSection("MongoDBSettings").GetValue<string>("ConnectionString");
                config.Database = Configuration.GetSection("MongoDBSettings").GetValue<string>("CoreDatabaseName");
            });

            services.AddSingleton<IDBContext,MongoDBContext>();
            services.AddSingleton<UserSystemDal>();
            services.AddSingleton<AppSettingsDal>();
            services.AddSingleton<UserDepartmentDal>();
            services.AddSingleton<EODRunningDal>();
            services.AddSingleton<EODReportDal>();
            services.AddSingleton<AlertsDal>();
            services.AddSingleton<ProvincesDal>();
            services.AddSingleton<IUserDal, UserDal>();
            services.AddSingleton<CitiesDal>();
            services.AddSingleton<PropertiesDal>();
            services.AddSingleton<BuildingsDal>();
            services.AddSingleton<RoleUserMappingDal>();
            services.AddSingleton<IUserRolesDal, UserRolesDal>();
            services.AddSingleton<ResourcesFlatDal>();
            services.AddSingleton<ResourcesDal>();
            services.AddSingleton<TeamsDal>();
            services.AddSingleton<EquipmentsDal>();
            services.AddSingleton<UserPermissionDal>();

            services.AddMediatR(typeof(DatabseLayerStartup).Assembly);
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseHttpsRedirection();
            app.UseCors(b =>
                        b.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithExposedHeaders("X-Total-Count")
                        );
            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/healthz", new HealthCheckOptions
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });

                endpoints.MapControllers();
                endpoints.MapGet("/awshealthchecker", async context =>
                {
                    await context.Response.WriteAsync(Configuration.GetValue<string>("AppName"));
                });
            });
        }
    }
}
