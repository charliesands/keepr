using System.ComponentModel.DataAnnotations;

namespace keepr.Models
{
  public class Vault
  {
    public int Id { get; set; }

    [Required]
    [MaxLength(20)]
    public string Name { get; set; }
    //required?
    [Required]
    public string Description { get; set; }

    // [Required]  
    public string UserId { get; set; }

    public Vault(string name, string description, string userid)
    {
      Name = name;
      Description = description;
      UserId = userid;
    }
    public Vault() { }
  }

}