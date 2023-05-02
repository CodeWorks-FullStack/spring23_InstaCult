namespace instaCult.Repositories;

public class CultMembersRepository
{
  private readonly IDbConnection _db;

  public CultMembersRepository(IDbConnection db)
  {
    _db = db;
  }

  internal int CreateCultMember(CultMember cultMemberData)
  {
    string sql = @"
    INSERT INTO cultMembers(accountId, cultId)
    VALUES(@AccountId, @CultId);
    SELECT LAST_INSERT_ID()
    ;";

    int id = _db.ExecuteScalar<int>(sql, cultMemberData);
    return id;
  }

  internal List<Cultist> GetCultistsInCult(int cultId)
  {
    string sql = @"
    SELECT
    cm.*,
    cultist.*
    FROM cultMembers cm
    JOIN accounts cultist ON cm.accountId = cultist.id
    WHERE cm.cultId = @cultId
    ;";

    List<Cultist> cultists = _db.Query<CultMember, Cultist, Cultist>
    (sql, (cultMember, cultist) =>
    {
      cultist.CultMemberId = cultMember.Id;
      return cultist;
    }, new { cultId }).ToList();

    return cultists;
  }
}
