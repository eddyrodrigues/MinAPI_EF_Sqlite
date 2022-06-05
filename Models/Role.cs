namespace Blog.Models;


public class Role
{
  public string Id { get; set; }
  public string Nome { get; set; }  
  public IList<User> Users { get; set; }  
}