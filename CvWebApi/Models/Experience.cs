using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CvWebApi.Models
{
  public enum Sector
  {
    Public,
    Private
  };

  public class Experience
  {
    public int ExperienceID { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime FinishDate { get; set; }
    public bool CurrentlyEmplyed { get; set; }

    public int CompanyId { get; set; }
    public virtual Company Company { get; set; }

    public int CvDocumentId { get; set; }
    public CvDocument CvDocument { get; set; }
  }
}
