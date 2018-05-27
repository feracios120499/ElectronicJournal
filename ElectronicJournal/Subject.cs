using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicJournal
{
    public class Subject
    {
        public string id { get; set; }
        public string Name { get; set; }
        public Subject(string id, string Name)
        {
            this.id = id;
            this.Name = Name;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
