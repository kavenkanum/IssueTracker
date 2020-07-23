using IssueTracker.Domain;
using IssueTracker.Domain.Entities;
using IssueTracker.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace IssueTracker.UnitTests.Queries
{
    public class TestFixture : IDisposable
    {
        private IConfigurationRoot _configuration;
        private static IServiceScopeFactory _scopeFactory;

        private IssueTrackerDbContext _issueTrackerDbContext { get; set; }
        public QueryDbContext QueryDbContext { get; private set; }

        public TestFixture()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables();
            _configuration = builder.Build();

            var services = new ServiceCollection();
            var startup = new Startup(_configuration);
            services.AddSingleton(Mock.Of<IWebHostEnvironment>(w =>
                w.EnvironmentName == "Development" &&
                w.ApplicationName == "IssueTracker"));

            startup.ConfigureServices(services);

            _scopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();
            var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<IssueTrackerDbContext>();

            //var optionsBuilder = new DbContextOptionsBuilder<IssueTrackerDbContext>();
            //optionsBuilder.UseSqlServer<IssueTrackerDbContext>(_configuration.GetConnectionString("IssueTrackerDb"));
            //_issueTrackerDbContext = new IssueTrackerDbContext(optionsBuilder.Options);

            QueryDbContext = new QueryDbContext(context);
        }

        public static async Task AddAsync<TEntity>(TEntity entity) where TEntity: class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<IssueTrackerDbContext>();
            context.Add(entity);

            await context.SaveChangesAsync();

        }

        public void Dispose()
        {
            _issueTrackerDbContext.Dispose();
        }
    }
}
