using Blog.Models;

namespace Blog.ViewModels;

public class UserViewModel
{
  public int Id { get; set; }
  public IList<Role> Roles { get; set; }  
  public string Nome { get; set; }
  public string Email { get; set; }
  public string Password { get; set; }
}