namespace instaCult.Services;

public class CultsService
{
  private readonly CultsRepository _repo;

  public CultsService(CultsRepository repo)
  {
    _repo = repo;
  }

  internal Cult CreateCult(Cult cultData)
  {
    int id = _repo.CreateCult(cultData);
    // TODO change once get by id is done
    cultData.Id = id;
    // 
    return cultData;
  }

  internal List<Cult> Get() // GET ALL
  {
    List<Cult> cults = _repo.GetAll();
    return cults;
  }

  internal List<Cult> Get(string search) // GET WITH string SEARCH
  {
    List<Cult> cults = _repo.GetAll(search);
    return cults;
  }

  internal Cult Get(int cultId) // our GET ONE
  {
    Cult cult = _repo.GetOne(cultId);
    return cult;
  }
}
