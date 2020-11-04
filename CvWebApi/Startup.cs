using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CvWebApi.Database;
using CvWebApi.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CvWebApi
{
  public class Startup
  {

    readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SqlConnection")));
      services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<ApplicationDbContext>();
      services.AddControllers();

      services.AddCors(options =>
      {
        options.AddPolicy(name: MyAllowSpecificOrigins,
                          builder =>
                          {
                            builder.WithOrigins("http://localhost:4200");
                          });
      });


      // Work around to prevent default action of redirecting to login when user is not authorized. 
      // As this in an API -  we do not have a login page and this causes status code 404 when it
      // should return status code 401.
      services.ConfigureApplicationCookie(o =>
      {
        o.Events = new CookieAuthenticationEvents()
        {
          OnRedirectToLogin = (ctx) =>
          {
            ctx.Response.StatusCode = 401;

            return Task.CompletedTask;
          },
          OnRedirectToAccessDenied = (ctx) =>
          {

            ctx.Response.StatusCode = 403;

            return Task.CompletedTask;
          }
        };
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseHttpsRedirection();

      app.UseRouting();
      app.UseCors(MyAllowSpecificOrigins);

      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });

      using (var service = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
      {
        ApplicationDbContext context = service.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context.Database.EnsureCreated();
      }
    }
  }
}
