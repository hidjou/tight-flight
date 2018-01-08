using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FlightData;
using Microsoft.EntityFrameworkCore;
using FlightServices;
using FlightData.Models;
using Hangfire;
using System;
using System.Diagnostics;

namespace TightFlight
{
    public class Startup
    {
        private IFlight _flight;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<AuthUser, ApplicationRole>(o =>
            {
                o.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!#$%&'*+-/=?^_`{|}~.@";
                // configure identity options
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 6;
            })
               .AddEntityFrameworkStores<FlightContext>();

            services.AddMvc();
            // Add a singleton instance of our config
            services.AddSingleton(Configuration);
            // Inject our services
            services.AddScoped<IPlane, PlaneService>();
            services.AddScoped<IEmployee, EmployeeService>();
            services.AddScoped<IFlight, FlightService>();

            services.AddTransient<IFlight, FlightService>();

            services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString("FlightConnection")));

            services.AddDbContext<FlightContext>(options => options
            .UseSqlServer(Configuration.GetConnectionString("FlightConnection"), builder => builder.MigrationsAssembly("FlightData")));

            var provider = services.BuildServiceProvider();
            _flight = provider.GetService<IFlight>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseAuthentication();

            app.UseStaticFiles();

            // Using hangfire
            GlobalConfiguration.Configuration.UseSqlServerStorage(Configuration.GetConnectionString("FlightConnection"));
            BackgroundJob.Enqueue(() => Console.WriteLine("Getting started with hangfire!"));
            app.UseHangfireDashboard();
            app.UseHangfireServer();

            // Check planes that are yet to take off
            RecurringJob.AddOrUpdate(() => _flight.CheckFlightsForTakeOff(), Cron.Minutely);
            // Check planes that are flying to land them if necessary
            RecurringJob.AddOrUpdate(() => _flight.CheckFlightsForLanding(), Cron.Minutely);
            // Check planes that are under maintenance for ending the maintenance
            RecurringJob.AddOrUpdate(() => _flight.CheckPlanesForOffMaintenance(), Cron.Minutely);
            // Generate random flights
            RecurringJob.AddOrUpdate(() => _flight.GenerateRandomFlight(), Cron.HourInterval(6));


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
