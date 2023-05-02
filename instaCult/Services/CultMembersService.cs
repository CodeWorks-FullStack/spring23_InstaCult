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

  internal CultMember GetOneCultMember(int cultMemberId)
  {
    CultMember cultMember = _repo.GetOneCultMember(cultMemberId);

    if (cultMember == null)
    {
      throw new Exception($"This was a bad ID buddy, you should try again: {cultMemberId}");
    }

    return cultMember;
  }

  internal string RemoveCultMember(int cultMemberId, string userId)
  {
    CultMember cultMember = GetOneCultMember(cultMemberId);

    if (cultMember.AccountId != userId)
    {
      throw new Exception("THIS IS NOT YOUR DATA TO REMOVE, BUD");
    }

    _repo.RemoveCultMember(cultMemberId);

    return $"CULT MEMBERSHIP WITH THIS CULT ID: {cultMember.CultId} HAS BEEN TERMINATED, YOU ARE FREE TO GO";
  }
}
