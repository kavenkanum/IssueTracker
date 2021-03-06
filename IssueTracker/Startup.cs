using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using IssueTracker.Domain.Entities;
using IssueTracker.Domain.Repositories;
using IssueTracker.Persistence;
using IssueTracker.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MediatR;
using IssueTracker.Commands;
using IssueTracker.Queries;
using IssueTracker.Domain.Language.ValueObjects;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Authorization;
using IssueTracker.Policies;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using IssueTracker.Domain.Language;
using System.Security.Principal;

namespace IssueTracker
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
            services.AddDbContext<IssueTrackerDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("IssueTrackerDB")));

            services.AddHttpContextAccessor();

            services.AddTransient<IProjectRepository, ProjectRepository>();
            services.AddTransient<IJobRepository, JobRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IDateTimeProvider, DateTimeProvider>();
            services.AddTransient<ClaimsPrincipal>(s =>
                s.GetService<IHttpContextAccessor>().HttpContext.User);
            services.AddScoped<CurrentUser>();
            services.AddTransient<QueryDbContext>();
            services.AddMediatR(typeof(GetListOfProjectsQuery).Assembly);
            services.AddMediatR(typeof(CreateCommentCommand).Assembly);

            services.AddControllers();

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "https://localhost:5000";
                    options.RequireHttpsMetadata = false;

                    options.Audience = "IssueTrackerApi";
                });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("IsAdmin", p =>
                    p.RequireRole("admin"));
            });
            services.AddCors(options =>
            {
                options.AddPolicy("default", policy =>
                {
                    policy.WithOrigins("http://localhost:5002")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });


            services.AddMvc(options =>
            {
                var authenticatedUserPolicy = new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .Build();

                options.Filters.Add(new AuthorizeFilter(authenticatedUserPolicy));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("default");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}")
                .RequireAuthorization();
            });
        }
    }
}
