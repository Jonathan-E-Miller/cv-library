using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CvWebApi.Database
{
  public class ApplicationDbContext : IdentityDbContext<IdentityUser>
  {
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {

    }
  }
}
