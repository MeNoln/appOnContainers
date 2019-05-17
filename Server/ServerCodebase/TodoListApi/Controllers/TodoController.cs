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
        private IRepo db;
        public TodoController(IRepo db)
        {
            this.db = db;
        }

        [HttpGet("")]
        public async Task<IEnumerable<TodoModel>> GetAll() => await db.GetAllTodos();

        [HttpGet("done")]
        public async Task<IEnumerable<TodoModel>> GetAllDone() => await db.GetAlreadyFinishedTodos();

        [HttpPost("add")]
        public IActionResult CreateTodo(TodoModel model)
        {
            db.Create(model);
            return Ok(StatusCodes.Status201Created);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTodo(int id, TodoModel model)
        {
            if (id != model.Id)
                return BadRequest();

            db.Update(model);
            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult RemoveTodo(int id)
        {
            db.Delete(id);
            return NoContent();
        }
    }
}