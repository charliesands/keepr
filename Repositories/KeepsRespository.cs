using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
  public class KeepsRepository
  {
    private IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    // GET ALL KEEPS
    public IEnumerable<Keep> GetAll()
    {
      return _db.Query<Keep>("SELECT * FROM keeps;");
    }

    //GET Keep BY ID
    public IEnumerable<Keep> GetById(string userId)
    {
      return _db.Query<Keep>("SELECT * FROM keeps WHERE userId = @userId", new { userId });
    }

    //CREATE KEEP

    public Keep Create(Keep keep)
    {
      int id = _db.ExecuteScalar<int>(@"
      INSERT INTO keeps (name, description, img, userid, isPrivate)
      VALUES (@Name, @Description, @Img, @UserId, @isPrivate);
      SELECT LAST_INSERT_ID();", keep
      );
      keep.Id = id;
      return keep;
    }

    //UPDATE KEEP
    public Keep Update(Keep keep)
    {
      _db.Execute(@"
      UPDATE keeps SET
      name = @Name, 
      description = @Description,
      img = @Img,
      isPrivate = @IsPrivate,
      views = @Views,
      shares = @Shares, 
      keeps = @Keeps 
      WHERE id = @Id", keep);
      return keep;
    }
    //   DELETE KEEP
    //WHERE id = @Id

    // name = @Name, 
    // description = @Description,
    // userId = @UserId,
    // img = @Img
    // isPrivate = @IsPrivate,
    // views = @Views,
    // shares = @Shares,

    // UPDATE keeps SET name, description, img, isPrivate, views, shares, keeps
    // VALUES @Name, @Description, @Img, @IsPrivate, @Views, @Shares, @Keeps

    public Keep Delete(Keep keep)
    {
      _db.Execute("DELETE FROM keeps WHERE id = @Id", keep);
      return keep;
    }

    public int Delete(int id)
    {
      return _db.Execute("DELETE FROM keeps WHERE id = @id", new { id });
    }
  }
}