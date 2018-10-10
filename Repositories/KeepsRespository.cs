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
    public Keep GetById(int id)
    {
      return _db.Query<Keep>("SELECT * FROM keeps WHERE id = @id", new { id }).FirstOrDefault();
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
    public Keep Update(int id, Keep keep)
    {
      _db.Execute(@"
    UPDATE keeps SET (name, description, userid, img, isPrivate, views, shares, keeps)
    VALUES (@Name, @Description, @UserId, @Img, @IsPrivate, @Views, @Shares, @Keeps)
    WHERE id = @Id
      ", keep);
      return keep;
    }

    // UPDATE vaults SET 
    //       name = @Name, 
    //       description = @Description,
    //       userid = @UserId,
    //       img = @Img
    //       isPrivate = @IsPrivate,
    //       views = @Views,
    //       shares = @Shares,
    //       keeps = @Keeps
    //       WHERE id = @Id
    //DELETE KEEP
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