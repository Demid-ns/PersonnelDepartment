using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelDepartment.Models
{
    public class Worker
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Photo { get; set; }
        public string Education { get; set; }
        public string Position { get; set; }
        public string Status { get; set; }
        public string Properties { get; set; }
    }
}
