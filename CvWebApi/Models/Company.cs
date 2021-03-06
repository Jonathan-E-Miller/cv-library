﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CvWebApi.Models
{
  public class Company
  {
    public int CompanyId { get; set; }
    
    [StringLength(50)]
    public string CompanyName { get; set; }
    public Sector Sector { get; set; }
  }
}
