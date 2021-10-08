using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseManagementSystem.Models;
using CourseManagementSystem.Tests;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using CourseManagementSystem.Models.Repositories;

namespace CourseManagementSystem
{
    public class Startup
    {
        public Startup(IConfiguration config)
        {
            Configuration = config;
        }
        public IConfiguration Configuration { get; set; }
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddDbContext<DataContext>(opts => {            
                opts.UseSqlServer(Configuration[
                "ConnectionStrings:CoursesConnection"]);
                opts.EnableSensitiveDataLogging(true);
            });
            services.AddControllers().AddNewtonsoftJson().AddXmlSerializerFormatters();
            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

            services.AddDbContext<IdentityContext>(opts =>
                opts.UseSqlServer(Configuration["ConnectionStrings:IdentityConnection"]));
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<IdentityContext>();
            services.Configure<IdentityOptions>(opts =>
            {
                opts.Password.RequiredLength = 6;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
                opts.User.RequireUniqueEmail = true;
                opts.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyz ";
            });
            services.AddScoped<EFCourseRepository>();
            services.AddScoped<EFStudentRepository>();
            services.AddScoped<EFProfessorRepository>();
            services.AddScoped<EFLessonRepository>();
            services.AddScoped<EFAttendanceRepository>();
            services.AddScoped<IdentityRepository>();
            services.AddControllers();
            services.AddAutoMapper(typeof(Startup).Assembly);

        }
        public void Configure(IApplicationBuilder app, DataContext dContext)//, IdentityContext iContext)
        {
            app.UseDeveloperExceptionPage();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => {
                endpoints.MapGet("/", async context => {
                    await context.Response.WriteAsync("Hello World!");
                });
                endpoints.MapControllers();
            });
            SeedData.SeedDatabase(dContext);

        }
    }

}
