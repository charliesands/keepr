using System.ComponentModel.DataAnnotations;

namespace keepr.Models
{
  public class VaultKeep
  {
    public int Id { get; set; }

    [Required]
    public int VaultId { get; set; }
    //required?
    [Required]
    public int KeepId { get; set; }

    // [Required]  
    public string UserId { get; set; }
    ///Id or ID????

    public VaultKeep(int vaultId, int keepId, string userid)
    {
      VaultId = vaultId;
      KeepId = keepId;
      UserId = userid;
    }
    public VaultKeep() { }
  }

}