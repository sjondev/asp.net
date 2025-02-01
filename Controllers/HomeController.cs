using Microsoft.AspNetCore.Mvc;

namespace TodoList.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet("/")] // -> simplificando o [Route] 
        // [Route("/")] -> Trabalhando com rotas 
        public string Get() {
            return "Olá pessoal";
        }
    }
}