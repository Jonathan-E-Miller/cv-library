using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CvWebApi.Models;

namespace CvWebApi.Database
{
  public class ApplicationDbContext : IdentityDbContext<IdentityUser>
  {
    public DbSet<CvDocument> CvDocuments { get; set; }
    public DbSet<SchoolAttendance> SchoolAttendances { get; set; }
    public DbSet<Qualification> Qualifications { get; set; }
    public DbSet<Company> Companies { get; set; }
    
    public DbSet<Experience> Experiences { get; set; }
    public DbSet<School> Schools { get; set; }
    public DbSet<Subject> Subjects { get; set; }

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {

    }
  }
}
