using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CvWebApi.Models
{
  public enum EducationLevel
  {
    GCSE,
    ALevel,
    Degree,
    Masters,
    PhD
  }

  public class Qualification
  {
    public int QualificationId { get; set; }
    public string Grade { get; set; }
    public DateTime Qualified { get; set; }

    public int SubjectId { get; set; }
    public virtual Subject Subject { get; set; }

    public int SchoolId { get; set; }
    public virtual School School { get; set; }

  }
}
