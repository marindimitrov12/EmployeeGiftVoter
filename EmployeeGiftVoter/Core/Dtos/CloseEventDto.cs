using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public class CloseEventDto
    {
        public int EventId { get; set; }
        public  string EndDate { get; set; }
        public int EmployeeId { get; set; }
    }
}
