using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicJournal
{
    public class Marks
    {
        public string Mark { get; set; }
        public DateTime Date { get; set; }
        public string IdStudent { get; set; }
        public Marks(string Mark,DateTime Date,string IdStudent)
        {
            this.Mark = Mark;
            this.Date = Date;
            this.IdStudent = IdStudent;
        }
    }
}
