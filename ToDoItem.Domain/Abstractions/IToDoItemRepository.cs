using System;
using System.Collections.Generic;
using System.Text;
using ToDoItem.Domain.Models;

namespace ToDoItem.Domain.Abstractions
{
    public interface IToDoItemRepository
    {
        public List<ToDoData> GetLists();
        public bool UpdateToDoItem(ToDoData toDoData);
        public int AddToDoItem(ToDoData toDoData);
        public bool DeleteToDoItem(int id);
    }
}
