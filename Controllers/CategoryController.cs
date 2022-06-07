using Data;
using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Memory;

namespace Todo.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class CategoryController : ControllerBase
  {

    /// <summary>
    /// {"data":[{"id":1,"name":"string","slug":"string"},{"id":2,"name":"string","slug":"string"},{"id":3,"name":"string","slug":"string"},{"id":4,"name":"string","slug":"strigiiansdsa"}],"errors":null}
    /// </summary>
    /// <returns></returns>
    [HttpGet("/")]
    // [Authorize(Roles = "Admin, Author")]
    public ActionResult<ResultViewModel<List<UserViewModel>>> get(
        [FromServices] IMemoryCache cache
    )
    {


      List<UserViewModel> usrs = cache.GetOrCreate("CategoriesCache", 
      entre => {
        entre.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(15);
        using (var context = new BlogDbContext())
        {
            List<User> cats = context
            .Users
            .Include(x => x.Roles)
            .ToList();

            List<UserViewModel> usrs = cats.Select(x => new UserViewModel { Id = x.Id, Nome = x.Nome, Roles = x.Roles.Select(r => new Role{ Id = r.Id, Nome = r.Nome}).ToList() }).ToList();
            return usrs;
        }
        });
      
        return Ok(new ResultViewModel<List<UserViewModel>>(usrs));
    }
    /// <summary>
    /// {"data":{"id":1,"name":"string","slug":"string"},"errors":null}
    /// </summary>
    /// <param name="id"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    [HttpGet("/{id:int}")]
    public ActionResult<ResultViewModel<Category>> GetById([FromRoute] int id,
      [FromServices] BlogDbContext context)
    {
      Category cat = context.Categories.Where(c => c.Id == id).FirstOrDefault();
      return Ok(new ResultViewModel<Category>(cat));
    }



    [HttpPost("/")]
    public ActionResult<ResultViewModel<Category>> Post([FromBody] Category cat,
      [FromServices] BlogDbContext context)
    {
      context.Categories.Add(cat);
      context.SaveChanges();
      return Ok(new ResultViewModel<Category>(cat));
    }



  }
}