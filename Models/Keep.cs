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

    public string Img { get; set; }

    public bool IsPrivate { get; set; } = false;

    public int Views { get; set; } = 0;  //= 0 will work?????

    public int Shares { get; set; } = 0;

    public int Keeps { get; set; } = 0;

    public Keep(string name, string description, string img, string userid)
    {
      Name = name;
      Description = description;
      Img = img;
      UserId = userid;
    }





  }

}