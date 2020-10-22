using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CvWebApi.Models
{
  public class CvDocument
  {
    public CvDocument()
    {
      SchoolAttendances = new List<SchoolAttendance>();
      Qualifications = new List<Qualification>();
      Experiences = new List<Experience>();
    }
    public int CvDocumentId { get; set; }

    [ForeignKey("IdentityUser")]
    public string UserId { get; set; }
    public virtual IdentityUser IdentityUser { get; set; }

    public List<SchoolAttendance> SchoolAttendances { get; set; }

    public List<Qualification> Qualifications { get; set; }

    public List<Experience> Experiences { get; set; }
  }
}
