using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Web
{
    public class Startup
    {
        //--------------------------------------
        //             Properties
        //--------------------------------------

        public IConfiguration _config { get; }


        //--------------------------------------
        //             Constructor
        //--------------------------------------

        public Startup(IConfiguration configuration)   // Constructor always named the same as the class that it's in. Does not have a return type. Used to initialise members of class.
        {
            _config = configuration;
        }

        //--------------------------------------
        //             Methods
        //--------------------------------------

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // DB Context
            var dbConnection = _config.GetConnectionString("DefaultConnectionString");
            services.AddDbContext<TodoListDbContext>(options => options.UseSqlServer(dbConnection));

            // MVC
            // services.AddMvc();        deprecated in .NET 5.0, use AddControllersWithViews instead
            services.AddControllersWithViews();

            // SPA
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            // CORS
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                                .WithExposedHeaders("Content-Disposition"); ;
                    });
            });

           // services.AddMvc(option => option.EnableEndpointRouting = false);
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

            // CORS
            app.UseCors("AllowAll");

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            // SPA
            app.UseSpaStaticFiles();

            // MVC
            //app.UseMvc(routes =>
            // {
            //   routes.MapRoute(
            //        name: "default",
            //        template: "{controller}/{action=Index}/{id?}");
            // });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            // SPA
            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

               // if (env.IsDevelopment())
               // {
                //    spa.UseAngularCliServer(npmScript: "start");
               // }
            });
        }
    }
}
