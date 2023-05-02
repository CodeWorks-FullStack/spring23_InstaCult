namespace instaCult.Repositories;

public class CultsRepository
{
  private readonly IDbConnection _db;

  public CultsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal int CreateCult(Cult cultData)
  {
    string sql = @"
    INSERT INTO cults
    (name, description, tags, leaderId)
    VALUES
    (@name, @description, @tags, @leaderId);
    SELECT LAST_INSERT_ID()
    ;";
    int id = _db.ExecuteScalar<int>(sql, cultData);
    return id;
  }

  internal void Edit(Cult cult)
  {
    string sql = @"
    UPDATE cults
    SET
    name = @Name,
    popularity = @Popularity
    WHERE id = @Id
    ;";

    _db.Execute(sql, cult);
  }

  internal List<Cult> GetAll()
  {
    string sql = @"
    SELECT
    c.*,
    leader.*
    from cults c
    JOIN accounts leader ON c.leaderId = leader.id;
    ";
    List<Cult> cults = _db.Query<Cult, Profile, Cult>(sql, (cult, leader) =>
    {
      cult.Leader = leader;
      return cult;
    }).ToList();
    return cults;
  }

  internal List<Cult> GetAll(string search)
  {
    search = '%' + search + '%';
    string sql = @"
    SELECT
    c.*,
    leader.*
    from cults c
    JOIN accounts leader ON c.leaderId = leader.id
    WHERE c.tags LIKE @search;
    ";
    List<Cult> cults = _db.Query<Cult, Profile, Cult>(sql, (cult, leader) =>
    {
      cult.Leader = leader;
      return cult;
    }, new { search }).ToList();
    return cults;
  }

  internal Cult GetOne(int cultId)
  {
    string sql = @"
    SELECT
    c.*,
    leader.*
    FROM cults c
    JOIN accounts leader ON c.leaderId = leader.id
    WHERE c.id = @cultId;
    ";
    Cult cult = _db.Query<Cult, Profile, Cult>(sql, (cult, leader) =>
    {
      cult.Leader = leader;
      return cult;
    }, new { cultId }).FirstOrDefault();
    return cult;
  }
}
