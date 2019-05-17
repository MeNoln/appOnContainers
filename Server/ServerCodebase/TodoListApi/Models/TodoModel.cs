using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoListApi.Models
{
    public class TodoModel
    {
        public int Id { get; set; }
        public string TaskName { get; set; }
        public bool IsDone { get; set; }
    }
}
