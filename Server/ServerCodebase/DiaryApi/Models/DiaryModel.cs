using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiaryApi.Models
{
    public class DiaryModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        //[Required]
        public string DayMark { get; set; }
        //[Required]
        public string DayDescription { get; set; }
    }
}
