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

    // [HttpGet("{userId}")]
    // public IEnumerable<Keep> GetById([FromRoute]string userId)
    // {
    //   return _repo.GetById(userId);
    // }

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

    [HttpPut]
    // [Authorize]
    public Keep Update([FromBody]Keep keep)
    {
      return _repo.Update(keep);
    }

    [HttpDelete("{id}")]
    public void Delete(int id)
    {
      _repo.Delete(id);
    }
  }
}
