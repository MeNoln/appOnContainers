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
        public string DayMark { get; set; }
        public string DayDescription { get; set; }
    }
}
