using Data;
using Models;
using ViewModel;
using Microsoft.AspNetCore.Mvc;

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
    public ActionResult<ResultViewModel<List<Category>>> get()
    {
      using (var context = new BlogDbContext())
      {
        List<Category> cats = context.Categories.ToList();

        return Ok(new ResultViewModel<List<Category>>(cats));
      }
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