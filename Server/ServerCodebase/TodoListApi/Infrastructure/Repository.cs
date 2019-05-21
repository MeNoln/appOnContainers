﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoListApi.DataAccess;
using TodoListApi.Models;

namespace TodoListApi.Infrastructure
{
    public class Repository : IRepo
    {
        private TodoAccessContext db;
        public Repository(TodoAccessContext db)
        {
            this.db = db;
        }

        //Get all unfinished todo`s
        public async Task<IEnumerable<TodoModel>> GetAllTodos() => await db.TodoModels.Where(i => i.IsDone == false)
                                                                            .OrderByDescending(i => i.Id).ToListAsync();

        //Get all finished todo`s
        public async Task<IEnumerable<TodoModel>> GetAlreadyFinishedTodos() => await db.TodoModels.Where(i => i.IsDone == true)
                                                                                        .OrderByDescending(i => i.Id).ToListAsync();
        //Create new todo
        public void Create(TodoModel model)
        {
            db.TodoModels.Add(model);
            db.SaveChanges();
        }

        //Delete finished todo from history
        public void Delete(int id)
        {
            var model = db.TodoModels.FirstOrDefault(i => i.Id == id);
            if (model != null)
                db.Remove(model);
            db.SaveChanges();
        }

        //Update current todo as finished and send it to history
        public void Update(TodoModel model)
        {
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
