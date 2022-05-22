using Microsoft.AspNetCore.Mvc;
using ToDoItem.Domain.Abstractions;
using ToDoItem.Domain.Models;

namespace ToDoList_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoItemController : Controller
    {
        private readonly IToDoItemService _toDoItemService;

        public ToDoItemController(IToDoItemService toDoItemService)
        {
            _toDoItemService = toDoItemService;
        }

                
        [HttpGet]
        [Route("GetLists")]
        public IActionResult GetLists()
        {
            var result = _toDoItemService.GetLists();
            if (result == null)
            {
                return NotFound("Item not found");
            }
            return Ok(result);
        }        

        
        [HttpPut]
        [Route("UpdateToDoItem")]
        public IActionResult UpdateToDoItem(ToDoData toDoData)
        {
            var isUpdated = _toDoItemService.UpdateToDoItem(toDoData);
            if (isUpdated == false)
            {
                return NotFound("item not found");
            }
            return Ok(isUpdated);
        }

                
        [HttpPost]
        [Route("AddToDoItem")]
        public IActionResult AddToDoItem(ToDoData toDoData)
        {
            var id = _toDoItemService.AddToDoItem(toDoData);
            toDoData.ListId = id;
            return Created("api/GetLists/" + id.ToString(), toDoData);
        }

                
        [HttpDelete]
        [Route("DeleteToDoItem/{id}")]
        public IActionResult DeleteToDoItem(int id)
        {
            var isDeleted = _toDoItemService.DeleteToDoItem(id);
            if (isDeleted == false)
            {
                return NotFound("List Item not found");
            }
            return Ok(isDeleted);
        }

    }
}
