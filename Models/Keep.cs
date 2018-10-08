using System;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace keepr.Models

{



  public class Keep

  {
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }

    public string UserId { get; set; }
    [Required]
    public string Img { get; set; }

    public int IsPrivate { get; set; }

    public int Views { get; set; }

    public int Shares { get; set; }

    public int Keeps { get; set; }

    public Keep(string name, string description, string img, string userid)
    {
      Name = name;
      Description = description;
      Img = img;
      UserId = userid;
      IsPrivate = 0;
    }

    public Keep()
    {
    }






  }

}