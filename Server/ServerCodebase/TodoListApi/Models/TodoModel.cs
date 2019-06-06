using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoListApi.Models
{
    public class TodoModel
    {
        public int Id { get; set; }
        [Required]
        public string TaskName { get; set; }
        public bool IsDone { get; set; }
        public string UserId { get; set; }
    }
}
