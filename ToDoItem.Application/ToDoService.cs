using System;
using System.Collections.Generic;
using System.Text;
using ToDoItem.Domain.Abstractions;
using ToDoItem.Domain.Models;

namespace ToDoItem.Application
{
    public class ToDoService : IToDoItemService
    {
        private readonly IToDoItemRepository _toDoItemRepository;

        public ToDoService(IToDoItemRepository toDoItemRepository)
        {
            _toDoItemRepository = toDoItemRepository;
        }
        public int AddToDoItem(ToDoData toDoData)
        {
            return _toDoItemRepository.AddToDoItem(toDoData);
        }

        public bool DeleteToDoItem(int id)
        {
            return _toDoItemRepository.DeleteToDoItem (id);
        }

        public List<ToDoData> GetLists()
        {
            return _toDoItemRepository.GetLists();
        }

        public bool UpdateToDoItem(ToDoData toDoData)
        {
            return _toDoItemRepository.UpdateToDoItem(toDoData);
        }
    }
}
