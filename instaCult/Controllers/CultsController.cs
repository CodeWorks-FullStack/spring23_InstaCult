namespace instaCult.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CultsController : ControllerBase
{
  private readonly CultsService _cultsService;

  private readonly CultMembersService _cultMembersService;
  private readonly Auth0Provider _auth;

  public CultsController(CultsService cultsService, Auth0Provider auth, CultMembersService cultMembersService)
  {
    _cultsService = cultsService;
    _auth = auth;
    _cultMembersService = cultMembersService;
  }

  [HttpPost]
  [Authorize]
  public async Task<ActionResult<Cult>> CreateCult([FromBody] Cult cultData)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      cultData.LeaderId = userInfo.Id;
      Cult cult = _cultsService.CreateCult(cultData);
      cult.Leader = userInfo;
      return Ok(cult);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet]
  public ActionResult<List<Cult>> GetCults(string search)
  {
    try
    {
      List<Cult> cults;
      if (search == null)
      {
        cults = _cultsService.Get(); // NOT necessary but works nicely for searching
      }
      else
      {
        cults = _cultsService.Get(search);
      }

      return Ok(cults);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }


  [HttpGet("{cultId}")]
  public ActionResult<Cult> GetOneCult(int cultId)
  {
    try
    {
      Cult cult = _cultsService.Get(cultId);
      return Ok(cult);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{cultId}/cultists")]
  public ActionResult<List<Cultist>> GetCultistsInCult(int cultId)
  {
    try
    {
      List<Cultist> cultists = _cultMembersService.GetCultistsInCult(cultId);
      return Ok(cultists);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
