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

        // GET: api/todo
        [HttpGet("")]
        public async Task<IEnumerable<TodoModel>> GetAll() => await db.GetAllTodos();

        //GET: api/todo/done
        [HttpGet("done")]
        public async Task<IEnumerable<TodoModel>> GetAllDone() => await db.GetAlreadyFinishedTodos();

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