using Blog.Services;
using Blog.ViewModels;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Todo.Controllers;

[Route("v1/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
  [HttpPost]
  public async Task<IActionResult> Post(
    [FromServices] BlogDbContext context,
    [FromBody] LoginViewModel model,
    [FromServices] TokenService tokenService
  )
  {
    
    if (!ModelState.IsValid)
      return BadRequest(new ResultViewModel<string>("Não entendemos os dados enviados - Favor tentar novamente com os dados corretos"));

    var user = context.Users.Where(x => x.Nome == model.login).Include(x => x.Roles).FirstOrDefault();

    if (user == null) return Ok(new ResultViewModel<string>("Usuário ou senha incorretos!"));

    var token = tokenService.GenerateToken(user);

    return Ok(new ResultViewModel<UserTokenViewModel>(new UserTokenViewModel{Token = token}));
  }
}