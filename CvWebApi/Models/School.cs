﻿using Microsoft.Data.SqlClient.DataClassification;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CvWebApi.Models
{
  public class School
  {
    public int SchoolId { get; set; }
    
    [Index(IsUnique = true)]
    [StringLength(50)]
    public string Name { get; set; }
  }
}
