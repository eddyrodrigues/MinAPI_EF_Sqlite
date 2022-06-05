using Blog.Services;
using Blog.ViewModels;
using Blog.Extensions;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Blog.Models;
using System.Net;

namespace Todo.Controllers;

[Route("v1/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
  [HttpPost("")]
  public async Task<IActionResult> Post(
    [FromServices] BlogDbContext context,
    [FromBody] LoginViewModel model,
    [FromServices] TokenService tokenService
  )
  {
    
    if (!ModelState.IsValid)
      return BadRequest(new ResultViewModel<List<string>>(ModelState.GetErrors()));

    var user = context.Users.Where(x => x.Email == model.Email && x.Password == model.Password).Include(x => x.Roles).FirstOrDefault();

    if (user == null) return Ok(new ResultViewModel<string>("Usuário ou senha incorretos!"));

    var token = tokenService.GenerateToken(user);

    return Ok(new ResultViewModel<UserTokenViewModel>(new UserTokenViewModel{Token = token, ExpiresIn = (int) DateTime.UtcNow.AddHours(8).Ticks}));
  }
  
  [HttpPost("new-user")]
  [Authorize(Roles ="Admin")]
  public async Task<IActionResult> Post(
    [FromServices] BlogDbContext context,
    [FromBody] UserViewModel model,
    [FromServices] TokenService tokenService
  )
  {
    
    if (!ModelState.IsValid)
      return BadRequest(new ResultViewModel<List<string>>(ModelState.GetErrors()));

    try{

      var userExists = await context.Users.FirstOrDefaultAsync(x => x.Email == model.Email);
      if (userExists != null) return Ok(new ResultViewModel<UserViewModel>("Já existe um usuário com existe e-mail. Não foi possível cadastrar esse o novo usuário com esse email"));
      
      User u = new User
      {
        Id = 0,
        Email = model.Email,
        Nome = model.Nome,
        Password = model.Password
      };
      
      await context.Users.AddAsync(u);
      await context.SaveChangesAsync();
      model.Id = u.Id;
      return Ok(new ResultViewModel<UserViewModel>(model));
    }catch{
      return StatusCode(((int)HttpStatusCode.InternalServerError),
      new ResultViewModel<UserViewModel>("Erro ao salvar o usuário. Favor tentar novamente em alguns instantes"));
    }
  }





}