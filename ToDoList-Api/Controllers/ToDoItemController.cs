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
        [Route("GetPaymentDetails")]
        public IActionResult GetLists()
        {
            var result = _toDoItemService.GetLists();
            if (result == null)
            {
                return NotFound("Payments not found");
            }
            return Ok(result);
        }        

        
        [HttpPut]
        [Route("UpdatePaymentDetail")]
        public IActionResult UpdateToDoItem(ToDoData paymentDetail)
        {
            var isUpdated = _toDoItemService.UpdateToDoItem(paymentDetail);
            if (isUpdated == false)
            {
                return NotFound("Payment not found");
            }
            return Ok(isUpdated);
        }

                
        [HttpPost]
        [Route("AddPaymentDetail")]
        public IActionResult AddToDoItem(ToDoData toDoData)
        {
            var id = _toDoItemService.AddToDoItem(toDoData);
            toDoData.ListId = id;
            return Created("api/GetLists/" + id.ToString(), toDoData);
        }

                
        [HttpDelete]
        [Route("DeletePaymentDetail/{id}")]
        public IActionResult DeletePaymentDetail(int id)
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
