using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using TodoList.Data;
using TodoList.Model;

namespace TodoList.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet("/")]
        public List<TodoItem> Get(
           [FromServices] AppDataContext context // isso é conhecido como uma dependencies injection (injeção de dependencia)
        ) {
            return context.Todos.ToList();
        }

        [HttpGet("/{id:int}")]
        public ActionResult<TodoItem> Get(int id, [FromServices] AppDataContext context) {            
            var item = context.Todos.FirstOrDefault(x => x.Id == id);

            if (item == null) {
                return BadRequest("Não foi encotrado nada");
            }

            return Ok(item);
        }

        [HttpPost("/")]
        public TodoItem Post(
            // Body é o corpo aonde vamos escrever o Json para fazer o post na web
            [FromBody] TodoItem item, 
            // Contexto é onde acessamos o contexto do Banco de dados para conseguir fazer as operações CRUD.
            // no .NET 8 nem precisa especificar o [FromServices] ou [FromBody] para o .NET porque
            // ele já é inteligente suficentente para saber que isso é um serviço ou um corpo de envio de dados.
            [FromServices] AppDataContext context)
        {
            context.Todos.Add(item);
            context.SaveChanges();
            return item;
        }

        [HttpPut("/{id:int}")]
        public ActionResult<TodoItem> Put(
            [FromRoute] int id,
            [FromBody] TodoItem item,
            [FromServices] AppDataContext context
        )
        {
            var model = context.Todos.FirstOrDefault(x => x.Id == id);
            if (model == null){
                return BadRequest("Valor invalido!");
            }

            model.Title = item.Title;
            model.Done = item.Done;

            context.Todos.Update(model);
            context.SaveChanges();

            return Ok(model);
        }

        [HttpDelete("/{id:int}")]
        public ActionResult<TodoItem> Delete([FromRoute] int id, [FromServices] AppDataContext context) 
        {
            var item = context.Todos.FirstOrDefault(x => x.Id == id);
            if(item == null) return BadRequest("Dado não encontrado");
            context.Todos.Remove(item);
            context.SaveChanges();
            return Ok($"item com o Id {id} foi deletado!");
        }
    }
}