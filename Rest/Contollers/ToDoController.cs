using Application.ToDo.Commands.CreateToDo;
using Application.ToDo.Commands.Queries.GetToDoItemDetail;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MyCleanArchitecture.Contollers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ToDoController : Controller
    {

        private ISender _mediator;

        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>(); 


        [HttpPost]
        public async Task<IActionResult> PostTodo(string name)
        {
            Guid? id = null;
            try
            {
                var request = new CreateToDoCommand { Name = name };
                id = await Mediator.Send(request);

            }
            catch (Exception e)
            {

                Console.WriteLine(e);
            }

            return Ok(id);
        }


        [HttpGet]
        public async Task<IActionResult> Get(string name)
        {
            ToDoItemVM toDo = null;
            try
            {
                var query = new GetToDoItemDetailQuery { Name = name };
                toDo = await Mediator.Send(query); 
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return Ok(toDo);
        }
    }
}
