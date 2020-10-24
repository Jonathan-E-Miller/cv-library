using CvWebApi;
using CvWebApi.Database;
using System;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace CvWebApiTests
{
  public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<Startup>
  {
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
      builder.ConfigureServices(services =>
      {
        // Create a new service provider.
        var serviceProvider = new ServiceCollection().AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

        // Add a database context (AppDbContext) using an in-memory database for testing.
        services.AddDbContext<ApplicationDbContext>(options =>
        {
          options.UseInMemoryDatabase("InMemoryAppDb");
          options.UseInternalServiceProvider(serviceProvider);
        });

        // Build the service provider.
        var sp = services.BuildServiceProvider();

        // Create a scope to obtain a reference to the database contexts
        using (var scope = sp.CreateScope())
        {
          var scopedServices = scope.ServiceProvider;
          var appDb = scopedServices.GetRequiredService<ApplicationDbContext>();

          var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();
          // Ensure the database is created.
          appDb.Database.EnsureCreated();

          // TODO Seed data here.
        }
      });
    }
  }
}