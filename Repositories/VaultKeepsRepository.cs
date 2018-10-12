using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keepr.Models;
using keepr.Controllers;

namespace keepr.Repositories
{
  public class VaultKeepsRepository
  {
    private IDbConnection _db;

    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    //GET ALL VAULTKEEPS
    public IEnumerable<VaultKeep> GetAll()
    {
      return _db.Query<VaultKeep>($"SELECT * FROM vaultkeeps;");
    }

    //GET VAULTKEEP BY ID
    public VaultKeep GetById(int id)
    {
      return _db.Query<VaultKeep>("SELECT * FROM vaultkeeps WHERE id = @id", new { id }).FirstOrDefault();
    }

    public IEnumerable<Keep> GetVaultKeeps(int vaultId)
    {
      return _db.Query<Keep>("SELECT * FROM vaultkeeps vk INNER JOIN keeps k ON k.id = vk.keepId WHERE (vaultId = @vaultId)", new { vaultId });
    }

    //     SELECT * FROM vaultkeeps vk
    // -- INNER JOIN keeps k ON k.id = vk.keepId 
    // -- WHERE (vaultId = @vaultId) 

    //CREATE VAULTKEEP

    public VaultKeep Create(VaultKeep vaultkeep)
    {
      int id = _db.ExecuteScalar<int>(@"
      INSERT INTO vaultkeeps (vaultId, keepId, userid)
      VALUES (@VaultId, @KeepId, @UserId);
      SELECT LAST_INSERT_ID();", vaultkeep
      );
      ///UserId or UserID?????? line 36
      vaultkeep.Id = id;
      return vaultkeep;
    }

    //UPDATE VAULTKEEP
    public VaultKeep Update(VaultKeep vaultkeep)
    {
      _db.Execute(@"
      UPDATE vaultkeeps SET (vaultId, keepId, userid) 
      VALUES (@VaultId, @KeepId, @UserId)
      WHERE id = @Id
      ", vaultkeep);
      return vaultkeep;
    }

    //DELETE VAULTKEEP
    public VaultKeep Delete(VaultKeep vaultkeep)
    {
      _db.Execute("DELETE FROM vaultkeeps WHERE id = @Id", vaultkeep);
      return vaultkeep;
    }

    public int Delete(int vaultId, int keepId)
    {
      return _db.Execute("DELETE FROM vaultkeeps WHERE (keepId = @keepId) AND ( vaultId = @vaultId);", new { vaultId, keepId });
    }

  }
}