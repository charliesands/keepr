using System;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace keepr.Models

{



  public class Keep

  {
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }

    public int Id { get; set; }

    public bool IsPrivate { get; set; } = false;

    public string Img { get; set; }

    public int Views { get; set; }

    public int Shares { get; set; }

    public int Keeps { get; set; }

    public Keep(string name, string description)
    {
      Name = name;
      Description = description;
    }





  }

}