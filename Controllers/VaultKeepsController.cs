using System.Collections.Generic;
using keepr.Models;
using keepr.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
  [Route("api/[controller]")]
  [ApiController]

  public class VaultKeepsController : Controller
  {
    VaultKeepsRepository _repo;
    public VaultKeepsController(VaultKeepsRepository repo)
    {
      _repo = repo;
    }

    [HttpGet]
    public IEnumerable<VaultKeep> Get()
    {
      return _repo.GetAll();
    }

    [HttpGet("{vaultId}")]

    public IEnumerable<Keep> GetVaultKeeps([FromRoute] int vaultId)
    {
      return _repo.GetVaultKeeps(vaultId);
    }

    // [Authorize] do I want this?
    [HttpPost]
    public VaultKeep Post([FromBody] VaultKeep vaultkeep)
    {
      if (ModelState.IsValid)
      {
        vaultkeep = new VaultKeep(vaultkeep.VaultId, vaultkeep.KeepId, vaultkeep.UserId);
        return _repo.Create(vaultkeep);
      }
      throw new System.Exception("Invalid VaultKeep");
    }

    [HttpDelete("vaults/{vaultId}/keeps/{keepId}")]
    public void Delete(int vaultId, int keepId)
    {
      _repo.Delete(vaultId, keepId);
    }

  }
}
