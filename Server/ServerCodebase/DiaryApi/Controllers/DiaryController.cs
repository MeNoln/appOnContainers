using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiaryApi.Infrastructure;
using DiaryApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiaryApi.Controllers
{
    [Route("api/diary")]
    [ApiController]
    public class DiaryController : Controller
    {
        private IRepo db; //Using database methods via DI
        public DiaryController(IRepo db)
        {
            this.db = db;
        }

        //GET: api/diary
        [HttpGet, Route("")]
        public async Task<IEnumerable<DiaryModel>> GetAll() => await db.GetAllDiaryNotes();

        //GET: api/diary/{id}
        [HttpGet, Route("{id}")]
        public async Task<IActionResult> GetCurrent(int id)
        {
            var model = await db.GetCurrentDiaryNote(id);
            if (model == null)
                return NotFound("Object not found");

            return Json(model);
        }

        //POST: api/diary/add
        [HttpPost, Route("add")]
        public IActionResult CreateDiaryNote(DiaryModel model)
        {
            db.Create(model);
            return Ok(StatusCodes.Status201Created);
        }

        //PUT: api/diary/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateDiary(int id, DiaryModel model)
        {
            if (id != model.Id)
                return BadRequest();

            db.Update(model);
            return NoContent();
        }

        //DELETE: api/diary/delete/{id}
        [HttpDelete, Route("delete/{id}")]
        public IActionResult RemoveDiaryNote(int id)
        {
            db.Delete(id);
            return NoContent();
        }
    }
}