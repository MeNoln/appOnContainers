using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoListApi.Models;

namespace TodoListApi.Infrastructure
{
    public interface IRepo
    {
        Task<IEnumerable<TodoModel>> GetAllTodos(string _id);
        Task<IEnumerable<TodoModel>> GetAlreadyFinishedTodos(string _id);
        void Create(TodoModel model);
        void Delete(int id);
        void Update(TodoModel model);
    }
}
