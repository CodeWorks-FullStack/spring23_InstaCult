namespace instaCult.Services;

public class CultMembersService
{
  private readonly CultMembersRepository _repo;

  public CultMembersService(CultMembersRepository repo)
  {
    _repo = repo;
  }

  internal CultMember CreateCultMember(CultMember cultMemberData)
  {
    int id = _repo.CreateCultMember(cultMemberData);
    cultMemberData.Id = id;
    return cultMemberData;
  }

  internal List<Cultist> GetCultistsInCult(int cultId)
  {
    List<Cultist> cultists = _repo.GetCultistsInCult(cultId);
    return cultists;
  }
}
