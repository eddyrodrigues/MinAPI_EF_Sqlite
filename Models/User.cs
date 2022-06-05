namespace Blog.Models;

public class User
{
  public int Id { get; set; }
  public string Nome { get; set; }
  public IList<Role> Roles { get; set;}
}