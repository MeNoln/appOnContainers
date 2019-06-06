using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DiaryApi.Models
{
    public class DiaryModel
    {
        public int Id { get; set; }
        public string Date { get; set; }
        [Required]
        public string DayMark { get; set; }
        [Required]
        public string DayDescription { get; set; }
        public string UserId { get; set; }
    }
}
