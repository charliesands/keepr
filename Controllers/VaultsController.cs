using System.Collections.Generic;
using keepr.Models;
using keepr.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
  [Route("api/[controller]")]
  [ApiController]

  public class VaultsController : Controller
  {
    VaultsRepository _repo;
    public VaultsController(VaultsRepository repo)
    {
      _repo = repo;
    }

    // [HttpGet]
    // public IEnumerable<Vault> Get()
    // {
    //   return _repo.GetAll();
    // }

    [HttpGet]
    public IEnumerable<Vault> GetAllByUserId()
    {
      var userId = HttpContext.User.Identity.Name;
      var vaults = _repo.GetUserVaults(userId);
      return vaults;

    }
    // [Authorize] do I want this?
    [HttpPost]
    public Vault Post([FromBody] Vault vault)
    {
      if (ModelState.IsValid)
      {
        vault = new Vault(vault.Name, vault.Description, vault.UserId);
        return _repo.Create(vault);
        /// need Username?
      }
      throw new System.Exception("Invalid Vault");
    }

  }
}
