using Data;
using Microsoft.AspNetCore.Mvc;
using Models;
using ViewModel;

namespace Todo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController: ControllerBase
    {

        [HttpGet("/")]
        public ActionResult<List<Category>> get(){
            using (var context = new BlogDbContext()){
                List<Category> cats = context.Categories.ToList();            
                
                return Ok(new ResultViewModel<List<Category>>(cats));
            }
        }

        [HttpPost("/")]
        public ActionResult<Category> Post([FromBody] Category cat,
        [FromServices] BlogDbContext context){
            context.Categories.Add(cat);
            context.SaveChanges();
            return Ok(new ResultViewModel<Category>(cat));
        }



    }
}