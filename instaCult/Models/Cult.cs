namespace instaCult.Models;

public class Cult : RepoItem<int>
{

  public string Name { get; set; }
  public string Description { get; set; }
  public string Tags { get; set; }
  public string LeaderId { get; set; }
  public int Popularity { get; set; }
  public Profile Leader { get; set; }

}
