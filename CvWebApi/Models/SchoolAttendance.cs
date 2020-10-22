using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CvWebApi.Models
{
  public class SchoolAttendance
  {
    public int SchoolAttendanceId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime FinishDate { get; set; }

    public int SchoolId { get; set; }
    public virtual School School { get; set; }

    public int CvDocumentId { get; set; }
    public CvDocument CvDocument { get; set; }
  }
}
