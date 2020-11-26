using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CvWebApi.Models
{
  public class Subject
  {
    public int SubjectId { get; set; }

    [StringLength(50)]
    public string Name { get; set; }
  }
}
