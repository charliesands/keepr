using System.ComponentModel.DataAnnotations;

namespace keepr.Models
{
  public class Vault
  {
    public int Id { get; set; }

    [Required]
    [MinLength(20)]
    public string Name { get; set; }
    //required?
    [Required]
    public string Description { get; set; }

    // [Required]  
    public string UserID { get; set; }
    ///Id or ID????

    public Vault(string name, string description, string userid)
    {
      Name = name;
      Description = description;
      UserID = userid;
    }
    public Vault() { }
  }

}