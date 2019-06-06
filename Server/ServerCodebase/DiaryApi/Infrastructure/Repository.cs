using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiaryApi.DataAccess;
using DiaryApi.Models;
using DiaryApi.Services;
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
        public async Task<IEnumerable<DiaryModel>> GetAllDiaryNotes(string _id)
        {
            _id = CipherClass.Decipher(_id);
            return await db.Diaries.Where(i => i.UserId == _id).OrderByDescending(i => i.Id).ToListAsync();
        }

        //Get current note(not using this method in client)
        public async Task<DiaryModel> GetCurrentDiaryNote(int id) => await db.Diaries.FirstOrDefaultAsync(i => i.Id == id);

        //Create new note
        public void Create(DiaryModel model)
        {
            model.UserId = CipherClass.Decipher(model.UserId);
            db.Diaries.Add(model);
            db.SaveChanges();
        }

        //Update note
        public void Update(DiaryModel model)
        {
            model.UserId = CipherClass.Decipher(model.UserId);
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
