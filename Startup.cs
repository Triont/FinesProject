using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Project5.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Project5.Controllers;
using Project5.Model;
using Microsoft.Extensions.Logging;
using Project5.Models;
using Project5.Mapping;
using static ClassLibrary4.ServiceCollectionExtensions;
using static ClassLibrary4.Model.Database_FinesContext;
using AutoMapper;

namespace Project5
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
            services.AddScoped<Logger<CarsController>>();
            services.AddScoped<IPersonRepo, PersonRepo>();
            services.AddScoped<PersonRepo>();

            services.AddScoped<ClassLibrary4.IUnitOfWork, ClassLibrary4.UnitOfWork>();
            services.AddScoped<ServicesLibs.IPersonService, ServicesLibs.PersonService>();

            services.AddScoped<CarRepo>();
            services.AddScoped<CarService>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<PersonService>();
            services.AddScoped<FineRepo>();
            services.AddScoped<FineService>();
          
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
            services.AddDbContext<ClassLibrary4.Model.Database_FinesContext>(options =>
         options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Scoped);
            ServiceProvider serviceProvider = services.BuildServiceProvider();
            services.AddDbContext<Database_FinesContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Scoped);
          
            ClassLibrary4.Model.Database_FinesContext appDbContext = serviceProvider.GetService<ClassLibrary4.Model.Database_FinesContext>();
            
            services.RegisterYourLibrary(appDbContext);

            // Auto Mapper Configurations
     var mapperConfig = new MapperConfiguration(mc =>
     {
         mc.AddProfile(new MappingDatas());
     });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
