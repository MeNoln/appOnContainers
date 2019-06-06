using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoListApi.Infrastructure;
using TodoListApi.Models;

namespace TodoListApi.Controllers
{
    [Route("api/todo")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private IRepo db; //Using database methods via DI
        public TodoController(IRepo db)
        {
            this.db = db;
        }

        // GET: api/todo/_id
        [HttpGet("{userId}")]
        public async Task<IEnumerable<TodoModel>> GetAll(string userId) => await db.GetAllTodos(userId);

        //GET: api/todo/done/_id
        [HttpGet("done/{userId}")]
        public async Task<IEnumerable<TodoModel>> GetAllDone(string userId) => await db.GetAlreadyFinishedTodos(userId);

        //POST: api/todo/add
        [HttpPost("add")]
        public IActionResult CreateTodo(TodoModel model)
        {
            db.Create(model);
            return Ok(StatusCodes.Status201Created);
        }

        //PUT: api/todo/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateTodo(int id, TodoModel model)
        {
            if (id != model.Id)
                return BadRequest();

            db.Update(model);
            return NoContent();
        }

        //DELETE api/todo/{id}
        [HttpDelete("delete/{id}")]
        public IActionResult RemoveTodo(int id)
        {
            db.Delete(id);
            return NoContent();
        }
    }
}