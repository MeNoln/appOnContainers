using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiaryApi.DataAccess;
using DiaryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DiaryApi.Infrastructure
{
    public class Repository : IRepo
    {
        private DiaryAccessContext db;
        public Repository(DiaryAccessContext db)
        {
            this.db = db;
        }

        //Get all diary notes
        public async Task<IEnumerable<DiaryModel>> GetAllDiaryNotes() => await db.Diaries.OrderByDescending(i => i.Id).ToListAsync();

        //Get current note(not using this method in client)
        public async Task<DiaryModel> GetCurrentDiaryNote(int id) => await db.Diaries.FirstOrDefaultAsync(i => i.Id == id);

        //Create new note
        public void Create(DiaryModel model)
        {
            db.Diaries.Add(model);
            db.SaveChanges();
        }

        //Update note
        public void Update(DiaryModel model)
        {
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
        }

        //Delete note
        public void Delete(int id)
        {
            var model = db.Diaries.FirstOrDefault(i => i.Id == id);
            if(model != null)
                db.Diaries.Remove(model);
            db.SaveChanges();
        }
    }
}
