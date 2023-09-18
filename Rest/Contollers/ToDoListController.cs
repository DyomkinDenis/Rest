using Application.DTO;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MyCleanArchitecture.HATEOAS;
using static Application.Services.ToDoListService;

namespace MyCleanArchitecture.Contollers
{
    [Route("api/todolist/")]
    [ApiController]
    [Filtres.ExceptionFilter]
    public class ToDoListController : Controller
    {
        private readonly IToDoListService _toDoListService;
        private readonly LinkGenerator _linkGenerator;

        public ToDoListController(IToDoListService toDoListService, LinkGenerator linkGenerator)
        {
            _toDoListService = toDoListService;
            _linkGenerator = linkGenerator;
        }


        [HttpPost]
        public IActionResult CreateToDoList(ToDoListDTO toDoList)
        {
            var id = _toDoListService.AddToDoList(toDoList);

            return new CreatedResult(_linkGenerator.GetUriByAction(HttpContext, nameof(GetToDoList), values: new {id}), id);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateToDoList(Guid id, ToDoListDTO toDoList)
        {
            toDoList.Id = id;
            _toDoListService.UpdateToDoList(toDoList);

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult ModifyToDoList(Guid id, ToDoListDTO toDoList)
        {
            toDoList.Id = id;
            _toDoListService.ModifyToDoList(toDoList);

            return NoContent();
        }

        [HttpGet]
        public IActionResult GetToDoLists([FromQuery]FilterAndPaginationRequest request)
        {
            var list = _toDoListService.GetToDoLists(request);

            var links = new List<Link>();
            var query = HttpContext.Request.QueryString;
            links.Add(new Link(_linkGenerator.GetUriByAction(HttpContext) + query, "self", "Get"));
            if (list.Any())
            {
                links.Add(new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(GetToDoList), values: new { list.First().Id }), "list", "Get")); 
                links.Add(new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(UpdateToDoList), values: new { list.First().Id }), "update list", "Put"));
                links.Add(new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(ModifyToDoList), values: new { list.First().Id }), "modify list", "Patch"));
            }
            links.Add(new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(CreateToDoList)), "new list", "Post"));

            return Ok(new
            {
                list,
                links
            });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteToDoList(Guid id)
        {
            _toDoListService.DeleteToDoList(id);

            return NoContent();
        }

        [HttpGet("{id}")]
        [HttpHead("{id}")]
        public IActionResult GetToDoList(Guid id)
        {
            var list = _toDoListService.GetToDoList(id);

            if(list == null)
            {
                return NotFound();
            }

            var links = new List<Link>();
            var query = HttpContext.Request.QueryString;
            links.Add(new Link(_linkGenerator.GetUriByAction(HttpContext) + query, "self", "Get"));
            links.Add(new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(GetToDoLists)), "all", "Get"));
            links.Add(new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(GetToDoListItems), values: new { id }), "items", "Get"));
            links.Add(new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(UpdateToDoList), values: new { id }), "update list", "Put"));
            links.Add(new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(ModifyToDoList), values: new { id }), "modify list", "Patch"));
            links.Add(new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(CreateToDoList)), "new list", "Post"));
            
            return Ok(new
            {
                list,
                links
            });
        }

        [HttpGet("{id}/items")]
        public IActionResult GetToDoListItems(Guid id)
        {
            var list = _toDoListService.GetToDoListItems(id);
            var links = new List<Link>();
            var query = HttpContext.Request.QueryString;
            links.Add(new Link(_linkGenerator.GetUriByAction(HttpContext) + query, "self", "Get"));
            links.Add(new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(GetToDoList), values: new {id}), "list", "Get"));
            links.Add(new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(GetToDoLists)), "all", "Get"));
            links.Add(new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(GetToDoListItems), values: new { id }), "items", "Get"));

            return Ok(new
            {
                list,
                links
            });
        }

        [HttpOptions("{id}")]
        public IActionResult OptionsResource(int id)
        {
            // Логика для определения разрешенных методов и заголовков
            Response.Headers.Add("Allow", "HEAD, GET, POST, PUT, PATCH, DELETE,  OPTIONS");
            Response.Headers.Add("Access-Control-Allow-Methods", "HEAD, GET, POST, PUT, PATCH, DELETE,  OPTIONS");
            Response.Headers.Add("Access-Control-Allow-Headers", "Accept, Authorization");

            return Ok();
        }
    }
}
