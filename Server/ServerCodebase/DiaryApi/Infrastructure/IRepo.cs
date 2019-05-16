using DiaryApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiaryApi.Infrastructure
{
    public interface IRepo
    {
        Task<IEnumerable<DiaryModel>> GetAllDiaryNotes();
        Task<DiaryModel> GetCurrentDiaryNote(int id);
        void Create(DiaryModel model);
        void Delete(int id);
        void Update(DiaryModel model);
    }
}
