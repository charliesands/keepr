using System.Collections.Generic;
using keepr.Models;
using keepr.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
  [Route("api/[controller]")]
  [ApiController]

  public class KeepsController : Controller
  {
    KeepsRepository _repo;
    public KeepsController(KeepsRepository repo)
    {
      _repo = repo;
    }

    [HttpGet]
    public IEnumerable<Keep> Get()
    {
      return _repo.GetAll();
    }

    // [Authorize] do I want this?
    [HttpPost]
    public Keep Post([FromBody] Keep keep)
    {
      if (ModelState.IsValid)
      {
        Keep newKeep = new Keep(keep.Name, keep.Description, keep.Img, keep.UserId);
        return _repo.Create(newKeep);
        /// need Username?
      }
      throw new System.Exception("Invalid Keep");
    }

  }
}
